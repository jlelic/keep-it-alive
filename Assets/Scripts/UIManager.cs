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
    // Start is called before the first frame update
    GameManager GM;

    float uiScale = 1;

    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGasBar();
        UpdateHeatBar();
        UpdateWaterBar();
        UpdatePowerBar();
        UpdateEngineBar();
    }

    private void UpdateGasBar()
    {
        GasBarContainer.sizeDelta = new Vector2(uiScale * GM.GasCapacity + 20, GasBarContainer.sizeDelta.y);
        GasBar.sizeDelta = new Vector2(uiScale * GM.GasLevel, GasBar.sizeDelta.y);
    }
    private void UpdatePowerBar()
    {
        PowerBar.sizeDelta = new Vector2(uiScale * GM.PowerLevel, PowerBar.sizeDelta.y);
    }
    private void UpdateWaterBar()
    {
        WaterBar.sizeDelta = new Vector2(uiScale * GM.WaterLevel, WaterBar.sizeDelta.y);
    }
    private void UpdateHeatBar()
    {
        HeatBar.sizeDelta = new Vector2(uiScale * GM.HeatLevel, HeatBar.sizeDelta.y);
    }
    private void UpdateEngineBar()
    {
        EngineBar.sizeDelta = new Vector2(uiScale * GM.EngineLevel, EngineBar.sizeDelta.y);
    }
}
