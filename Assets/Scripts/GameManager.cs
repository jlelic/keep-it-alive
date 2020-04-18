using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlaying { get; private set; } = true;
    public float CarSpeed { get; private set; } = 0f;
    public float MaxSpeed { get; private set; } = 0.06f;
    public float SlowDown { get; private set; } = 0.001f;
    public float SpeedUp{ get; private set; } = 0.0005f;
    public float GasCapacity { get; private set; } = 100f;
    public float GasLevel { get; private set; } = 100f;
    public float GasConsumption { get; private set; } = 0.06f;



    RoadsManager roadsManager;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<WiperManager>().Wipe();
        }
    }

    void FixedUpdate()
    {
        GasLevel -= GasConsumption;
        GasLevel = Mathf.Max(GasLevel, 0);
        if (GasLevel <= 0)
        {
            CarSpeed = Mathf.Max(0f, CarSpeed-SlowDown);
        } else
        {
            CarSpeed = Mathf.Min(MaxSpeed, CarSpeed + SpeedUp);
        }

    }
}
