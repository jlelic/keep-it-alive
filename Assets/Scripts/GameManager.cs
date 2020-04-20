using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlaying { get; private set; } = true;
    public float CarSpeed { get; private set; } = 0f;
    public float ActualMaxSpeed { get; private set; } = 0.1f;
    public float MaxSpeed { get; private set; } = 0.1f;
    public float SlowDown { get; private set; } = 0.001f;
    public float SpeedUp{ get; private set; } = 0.0005f;
    public float GasCapacity { get; private set; } = 100f;
    public float GasLevel { get; private set; } = 100f;
    public float GasConsumption { get; private set; } = 0.01f;
    public float WaterEvaporation { get; private set; } = 0.015f;
    public float PowerConsumption { get; private set; } = 0.005f;
    public float HeatIncrease { get; private set; } = 0.02f;
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
    public int Repairing { get; private set; }

    RoadsManager roadsManager;
    WiperManager wiperManager;
    ParticleSystemsManager particleManager;

    bool isOverheating;

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
        particleManager = FindObjectOfType<ParticleSystemsManager>();
    }

    void Update()
    {
        if (!wiperManager.IsWiping && Input.GetKeyDown(KeyCode.Space))
        {
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
        }
    }

    void FixedUpdate()
    {
        MaxSpeed = EngineLevel > 40 ? ActualMaxSpeed : ActualMaxSpeed * EngineLevel / 40f;
        EngineLevel = Mathf.Clamp(EngineLevel, 0, 100);

        GasLevel -= GasConsumption;
        GasLevel = Mathf.Clamp(GasLevel, 0, 100);
        if (GasLevel <= 0)
        {
            CarSpeed = Mathf.Max(0f, CarSpeed-SlowDown);
        } else
        {
            CarSpeed = Mathf.Min(MaxSpeed, CarSpeed + SpeedUp);
        }


        PowerLevel -= PowerConsumption;
        PowerLevel = Mathf.Clamp(PowerLevel, 0, 100);

        WaterLevel -= HeatLevel > 80 ? (HeatLevel-80) /20f * WaterEvaporation : 0;
        WaterLevel = Mathf.Clamp(WaterLevel, 0, 100);

        HeatLevel += HeatIncrease;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 100);
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
    }

    public void TakeHit()
    {
        EngineLevel -= HitDamage;
        EngineLevel = Mathf.Max(0, EngineLevel);
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
                particleManager.ApplyIcecream();
                StartCoroutine(ApplyIcecream());
                break;
            case ItemType.PHONE:
                particleManager.ChargeBattery();
                StartCoroutine(ApplyPhone());
                break;
            case ItemType.WATER:
                particleManager.RefillWater();
                StartCoroutine(ApplyWater());
                break;
            case ItemType.WRENCH:
                particleManager.Repair();
                StartCoroutine(ApplyWrench());
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
}
