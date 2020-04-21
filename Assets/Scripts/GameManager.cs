using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject shipExplosion;

    [SerializeField] Text progressText;
    [SerializeField] Image gameCompletedOverlay;
    [SerializeField] Image gameOverOverlay;
    [SerializeField] Text gameOverText;
    [SerializeField] GameObject endGame;
    [SerializeField] GameObject LastRoad;
    public static GameManager Instance { get; private set; }
    public bool IsPlaying { get; private set; } = true;
    public float CarSpeed { get; private set; } = 0f;
    public float ActualMaxSpeed { get; private set; } = 0.05f;
    public float MaxSpeed { get; private set; } = 0.05f;
    public float SlowDown { get; private set; } = 0.001f;
    public float SpeedUp{ get; private set; } = 0.0005f;
    public float GasCapacity { get; private set; } = 100f;
    public float GasLevel { get; private set; } = 100f;
    public float GasConsumption { get; private set; } = 0.015f;
    public float WaterEvaporation { get; private set; } = 0.015f;
    public float PowerConsumption { get; private set; } = 0.005f;
    public float HeatIncrease { get; private set; } = 0.015f;
    public float HeatLevel { get; private set; } = 20f;
    public float PowerLevel { get; private set; } = 100f;
    public float WaterLevel { get; private set; } = 100f;
    public float EngineLevel { get; private set; } = 100f;
    public float WiperWaterCost { get; private set; } = 24f;
    public float WiperPowerCost { get; private set; } = 2f;
    public float HitDamage { get; private set; } = 30;
    public int CoolingDown { get; private set; }
    public int ChargingBattery { get; private set; }
    public int RefillingWater { get; private set; }
    public int RefillingGas{ get; private set; }
    public int Repairing { get; private set; }
    public float Progress { get; private set; }
    public bool IsGameCompleted { get; private set; }

    public bool InMenu = true;
    public bool TutorialMode = true;
    public bool EndlessMode = false;
    float timeElapsed = 0;

    RoadsManager roadsManager;
    WiperManager wiperManager;
    ParticleSystemsManager particleManager;
    WarningManager warningManager;
    TutorialManager tutorialManager;

    bool isOverheating;
    bool isGameOver;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        roadsManager = GetComponent<RoadsManager>();
        roadsManager.StartSpawning();
        wiperManager = GetComponent<WiperManager>();
        tutorialManager = GetComponent<TutorialManager>();
        warningManager = FindObjectOfType<WarningManager>();
        particleManager = FindObjectOfType<ParticleSystemsManager>();
    }

    void Update()
    {
        if(!IsGameCompleted && !isGameOver && (EngineLevel <= 0 || HeatLevel >=100 || GasLevel <= 0))
        {
            StartCoroutine(GameOver(GasLevel > 0));
            isGameOver = true;
        }

        if (!wiperManager.IsWiping && Input.GetKeyDown(KeyCode.W))
        {
            tutorialManager.OnWindowCleaned();
            if(PowerLevel < WiperPowerCost)
            {
                return;
            }
            PowerLevel -= WiperPowerCost;
            wiperManager.Wipe(WaterLevel >= WiperWaterCost);
            if (WaterLevel >= WiperWaterCost)
            {
                WaterLevel -= WiperWaterCost;
            }
            else
            {
                warningManager.addWarning(WarningMessageType.WATER_INSUFFICIENT, 3);
            }
        }
        
        if((IsGameCompleted || isGameOver) && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }

        if(!EndlessMode && Progress >= 2000)
        {
            roadsManager.ForcedSpawn = LastRoad;
        }
    }

    void FixedUpdate()
    {
        if(InMenu)
        {
            WaterLevel = 100;
            PowerLevel = 100;
            CarSpeed = MaxSpeed;
            return;
        }
        else if(!TutorialMode)
        {
            progressText.text = Mathf.RoundToInt(Progress) + " m";
            timeElapsed += Time.fixedDeltaTime;
            ActualMaxSpeed = 0.05f + timeElapsed * 0.0002f;
        }

        MaxSpeed = EngineLevel > 40 ? ActualMaxSpeed : ActualMaxSpeed * EngineLevel / 40f;
        if(GasLevel <=0 )
        {
            MaxSpeed = 0;
        }

        if(!TutorialMode)
        {
            GasLevel -= GasConsumption;
            PowerLevel -= PowerConsumption;
            WaterLevel -= HeatLevel > 80 ? (HeatLevel - 80) / 20f * WaterEvaporation : 0;
            HeatLevel += HeatIncrease;
        }
        else
        {
            GasLevel -= GasConsumption/8f;
            PowerLevel -= PowerConsumption/8f;
            HeatLevel += HeatIncrease/8f;
        }

        if (GasLevel <= 0)
        {
            CarSpeed = Mathf.Max(0f, CarSpeed-SlowDown);
        } else
        {
            CarSpeed = Mathf.Min(MaxSpeed, CarSpeed + SpeedUp);
        }

        if (isOverheating)
        {
            if(HeatLevel < 80)
            {
                isOverheating = false;
                particleManager.CoolDown();
            }
        }
        else
        {
            if (HeatLevel > 80)
            {
                isOverheating = true;
                particleManager.StartOverheating();
            }
        }

        EngineLevel = Mathf.Clamp(EngineLevel, 0, 100);
        GasLevel = Mathf.Clamp(GasLevel, 0, 100);
        PowerLevel = Mathf.Clamp(PowerLevel, 0, 100);
        WaterLevel = Mathf.Clamp(WaterLevel, 0, 100);
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 100);

        Progress += CarSpeed;

    }

    public void StartTheGame()
    {
        timeElapsed = 0;
        TutorialMode = false;
        roadsManager.RoadPrefabs.Clear();
        Progress = 0;
        foreach (var prefab in roadsManager.GamePrefabs)
        {
            roadsManager.RoadPrefabs.Add(prefab);
        }
    }

    IEnumerator GameOver(bool withExplosion)
    {
        isGameOver = true;
        if (withExplosion)
        {
            shipExplosion.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        if(!withExplosion && GasLevel > 0)
        {
            isGameOver = false;
            yield break;
        }
        gameOverOverlay.gameObject.SetActive(true);
        foreach(var graphic in gameOverOverlay.gameObject.GetComponentsInChildren<Graphic>())
        {
            if(graphic == gameOverOverlay)
            {
                continue;
            }
            graphic.color = Color.clear;
            Utils.tweenColor(graphic, Color.white, 2,1.5f);
        }
        gameOverOverlay.color = Color.clear;
        Utils.tweenColor(gameOverOverlay, Color.black, 2);
    }

    public void GameCompleted()
    {
        if(IsGameCompleted)
        {
            return;
        }
        IsGameCompleted = true;
        endGame.SetActive(true);
        gameCompletedOverlay.gameObject.SetActive(true);
        foreach (var graphic in gameCompletedOverlay.gameObject.GetComponentsInChildren<Graphic>())
        {
            if (graphic == gameCompletedOverlay)
            {
                continue;
            }
            graphic.color = Color.clear;
            Utils.tweenColor(graphic, Color.white, 2, 6);
        }
        gameCompletedOverlay.color = Color.clear;
        Utils.tweenColor(gameCompletedOverlay, Color.black, 2, 4);
    }



    public void TakeHit()
    {
        EngineLevel -= HitDamage;
        EngineLevel = Mathf.Max(0, EngineLevel);
        warningManager.addWarning(WarningMessageType.ENGINE_DAMAGED, 2);
        StartCoroutine(FreezeTime());
    }

    IEnumerator FreezeTime()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
    }

    public void DirtyWindow()
    {
        wiperManager.Dirty();
    }

    public void ApplyItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.ICECREAM:
                tutorialManager.OnIcecreamUsed();
                particleManager.ApplyIcecream();
                StartCoroutine(ApplyIcecream());
                break;
            case ItemType.PHONE:
                tutorialManager.OnBateryRecharged();
                particleManager.ChargeBattery();
                StartCoroutine(ApplyPhone());
                break;
            case ItemType.WATER:
                tutorialManager.OnWaterRefilled();
                particleManager.RefillWater();
                StartCoroutine(ApplyWater());
                break;
            case ItemType.WRENCH:
                tutorialManager.OnShipRepaired();
                particleManager.Repair();
                StartCoroutine(ApplyWrench());
                break;
            case ItemType.GAS:
                tutorialManager.OnGasRefilled();
                particleManager.RefillGass();
                StartCoroutine(ApplyGas());
                break;
        }
    }

    private IEnumerator ApplyIcecream()
    {
        CoolingDown++;
        for (int i = 0; i < 25; i++)
        {
            HeatLevel--;
            yield return new WaitForSeconds(0.05f);
        }
        CoolingDown--;
    }

    private IEnumerator ApplyPhone()
    {
        ChargingBattery++;
        for (int i = 0; i < 20; i++)
        {
            PowerLevel++;
            yield return new WaitForSeconds(0.05f);
        }
        ChargingBattery--;
    }

    private IEnumerator ApplyWater()
    {
        RefillingWater++;
        for (int i = 0; i < 20; i++)
        {
            WaterLevel++;
            yield return new WaitForSeconds(0.05f);
        }
        RefillingWater--;
    }

    private IEnumerator ApplyWrench()
    {
        Repairing++;
        for (int i = 0; i < 35; i++)
        {
            EngineLevel++;
            yield return new WaitForSeconds(0.05f);
        }
        Repairing--;
    }
    private IEnumerator ApplyGas()
    {
        RefillingGas++;
        for (int i = 0; i < 20; i++)
        {
            GasLevel++;
            yield return new WaitForSeconds(0.05f);
        }
        RefillingGas--;
    }
}
