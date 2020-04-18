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
    public float PowerConsumption{ get; private set; } = 0.005f;
    public float HeatLevel { get; private set; } = 20f;
    public float PowerLevel { get; private set; } = 100f;
    public float WaterLevel { get; private set; } = 100f;
    public float EngineLevel { get; private set; } = 100f;
    public float WiperWaterCost { get; private set; } = 20f;
    public float HitDamage { get; private set; } = 40f;

    RoadsManager roadsManager;
    WiperManager wiperManager;

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
    }

    void Update()
    {
        if (!wiperManager.IsWiping && Input.GetKeyDown(KeyCode.Space))
        {
            if(WaterLevel >= WiperWaterCost)
            {
                WaterLevel -= WiperWaterCost;
                wiperManager.Wipe();
            }
        }
    }

    void FixedUpdate()
    {
        MaxSpeed = EngineLevel > 40 ? ActualMaxSpeed : ActualMaxSpeed * EngineLevel / 40f;

        GasLevel -= GasConsumption;
        GasLevel = Mathf.Max(GasLevel, 0);
        if (GasLevel <= 0)
        {
            CarSpeed = Mathf.Max(0f, CarSpeed-SlowDown);
        } else
        {
            CarSpeed = Mathf.Min(MaxSpeed, CarSpeed + SpeedUp);
        }


        PowerLevel -= PowerConsumption;
        PowerLevel = Mathf.Max(PowerLevel, 0);

        WaterLevel -= HeatLevel > 50 ? HeatLevel /100f * WaterEvaporation : 0;
        WaterLevel = Mathf.Max(WaterLevel, 0);
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
}
