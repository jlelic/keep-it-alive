using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : MonoBehaviour
{
    public GameObject RoadPrefab;

    public float DespawnTreshold = -20;
    public float RoadSpacing = 10;
    
    List<Road> roadList = new List<Road>();

    GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;
    }

    public void StartSpawning()
    {
        for(var y = DespawnTreshold;y<=-DespawnTreshold;y+=RoadSpacing)
        {
            SpawnRoadAt(y);
        }
    }


    void FixedUpdate()
    {
        if (!GM.IsPlaying)
        {
            return;
        }

        // move roads
        foreach(var road in roadList)
        {
            road.transform.position -= new Vector3(0, GM.CarSpeed, 0);
        }

        if(roadList[0].transform.position.y < DespawnTreshold)
        {
            SpawnRoadAt(roadList[roadList.Count - 1].transform.position.y + RoadSpacing);
            Destroy(roadList[0].gameObject);
            roadList.RemoveAt(0);
        }
    }

    private void SpawnRoadAt(float y)
    {
        var newRoad = GameObject.Instantiate(RoadPrefab, new Vector3(0, y, 10), Quaternion.identity);
        roadList.Add(newRoad.GetComponent<Road>());
    }
}
