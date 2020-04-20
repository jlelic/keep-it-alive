using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform GasBarContainer;
    public RectTransform GasBar;
    public RectTransform HeatBar;
    public RectTransform WaterBar;
    public RectTransform PowerBar;
    public RectTransform EngineBar;

    [SerializeField] Color historyBarColor;
    [SerializeField] Color barContainerColor;
    [SerializeField] Color barContainerImprovingColor;

    Image gasContainer;
    Image powerContainer;
    Image waterContainer;
    Image heatContainer;
    Image engineContainer;

    RectTransform historyWaterBar;
    RectTransform historyEngineBar;
    Queue<float> waterHistory = new Queue<float>();
    Queue<float> engineHistory = new Queue<float>();
    float lastOldWaterValue = 100;
    float lastOldEngineLevel = 100;

    GameManager GM;

    float uiScale = 1;

    bool gasWarning;
    bool heatWarning;
    bool waterWarning;
    bool powerWarning;
    bool engineWarning;

    void Start()
    {
        GM = GameManager.Instance;
        historyWaterBar = MakeHistoryBar(WaterBar);
        historyEngineBar = MakeHistoryBar(EngineBar);

        gasContainer = GasBar.parent.GetComponent<Image>();
        powerContainer = PowerBar.parent.GetComponent<Image>();
        waterContainer = WaterBar.parent.GetComponent<Image>();
        heatContainer = HeatBar.parent.GetComponent<Image>();
        engineContainer = EngineBar.parent.GetComponent<Image>();
    }

    private RectTransform MakeHistoryBar(RectTransform bar)
    {
        var oldBar = Instantiate(bar);
        oldBar.transform.SetParent(bar.transform.parent);
        oldBar.position = bar.position;
        oldBar.SetAsFirstSibling();
        oldBar.GetComponent<Image>().color = historyBarColor;
        return oldBar.GetComponent<RectTransform>();
    }

    private void SetBarValue(RectTransform bar, float value)
    {
        bar.sizeDelta = new Vector2(uiScale * value, bar.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.PowerLevel > 0)
        {
            UpdateGasBar();
            UpdateHeatBar();
            UpdateWaterBar();
            UpdatePowerBar();
            UpdateEngineBar();
        }
        else
        {
            SetBarValue(GasBar, 0);
            SetBarValue(HeatBar, 0);
            SetBarValue(WaterBar, 0);
            SetBarValue(PowerBar, 0);
            SetBarValue(EngineBar, 0);
            SetBarValue(historyEngineBar, 0);
            SetBarValue(historyWaterBar, 0);
        }
    }

    private void StartWarning(RectTransform bar)
    {
        Utils.tweenColor(bar.transform.parent.GetComponent<Image>(), Color.red, 0.3f, 0, iTween.EaseType.easeInOutQuad, iTween.LoopType.pingPong);
    }

    private void UpdateGasBar()
    {
        if(!gasWarning)
        {
            if(GM.GasLevel < 20)
            {
                StartWarning(GasBar);
                gasWarning = true;
            }
        }
        else if(GM.GasLevel > 20)
        {
            iTween.Stop(GasBar.parent.gameObject);
            gasContainer.color = barContainerColor;
            gasWarning = false;
        }

        GasBarContainer.sizeDelta = new Vector2(uiScale * GM.GasCapacity + 20, GasBarContainer.sizeDelta.y);
        SetBarValue(GasBar, GM.GasLevel);
    }
    private void UpdatePowerBar()
    {
        if (!powerWarning)
        {
            if (GM.PowerLevel < 15)
            {
                StartWarning(PowerBar);
                powerWarning = true;
            }
        }
        else if (GM.PowerLevel > 15)
        {
            iTween.Stop(PowerBar.parent.gameObject);
            powerContainer.color = barContainerColor;
            powerWarning = false;
        }

        if (GM.ChargingBattery > 0)
        {
            powerContainer.color = barContainerImprovingColor;
        }
        else if (powerContainer.color == barContainerImprovingColor) {
            powerContainer.color = barContainerColor;
        }

        PowerBar.sizeDelta = new Vector2(uiScale * GM.PowerLevel, PowerBar.sizeDelta.y);
    }
    private void UpdateWaterBar()
    {
        if (!waterWarning)
        {
            if(GM.WaterLevel < GM.WiperWaterCost)
            {
                StartWarning(WaterBar);
                waterWarning = true;
            }
        }
        else if (GM.WaterLevel > GM.WiperWaterCost)
        {
            iTween.Stop(WaterBar.parent.gameObject);
            waterContainer.color = barContainerColor;
            waterWarning = false;
        }

        historyWaterBar.sizeDelta = new Vector2(uiScale * GM.WaterLevel, WaterBar.sizeDelta.y);
        SetBarValue(historyWaterBar, lastOldWaterValue);
        SetBarValue(WaterBar, GM.WaterLevel);
    }
    private void UpdateHeatBar()
    {
        if (!heatWarning)
        {
            if (GM.HeatLevel > 80)
            {
                StartWarning(HeatBar);
                heatWarning = true;
            }
        }
        else if (GM.HeatLevel < 80)
        {
            iTween.Stop(HeatBar.parent.gameObject);
            heatContainer.color = barContainerColor;
            heatWarning = false;
        }

        if (GM.CoolingDown > 0)
        {
            heatContainer.color = barContainerImprovingColor;
        }
        else if (heatContainer.color == barContainerImprovingColor)
        {
            heatContainer.color = barContainerColor;
        }

        SetBarValue(HeatBar, GM.HeatLevel);
    }
    private void UpdateEngineBar()
    {
        if (!engineWarning)
        {
            if (GM.EngineLevel < GM.HitDamage)
            {
                StartWarning(EngineBar);
                engineWarning = true;
            }
        }
        else if (GM.EngineLevel > GM.HitDamage)
        {
            iTween.Stop(EngineBar.parent.gameObject);
            engineContainer.color = barContainerColor;
            engineWarning = false;
        }


        if (GM.Repairing > 0)
        {
            engineContainer.color = barContainerImprovingColor;
        }
        else if (engineContainer.color == barContainerImprovingColor)
        {
            engineContainer.color = barContainerColor;
        }

        SetBarValue(historyEngineBar, lastOldEngineLevel);
        SetBarValue(EngineBar, GM.EngineLevel);
    }

    void FixedUpdate()
    {
        waterHistory.Enqueue(GM.WaterLevel);
        if (waterHistory.Count > 60)
        {
            waterHistory.Dequeue();
        }
        lastOldWaterValue = Mathf.Lerp(waterHistory.Peek(), lastOldWaterValue, 0.9f);

        engineHistory.Enqueue(GM.EngineLevel);
        if (engineHistory.Count > 60)
        {
            engineHistory.Dequeue();
        }
        lastOldEngineLevel = Mathf.Lerp(engineHistory.Peek(), lastOldEngineLevel, 0.9f);
    }
}
