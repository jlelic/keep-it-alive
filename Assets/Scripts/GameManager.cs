using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlaying { get; private set; } = true;
    public float CarSpeed { get; private set; } = 0.06f;

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

}
