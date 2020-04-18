using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float spawnInterval = 4.0f;
    public GameObject[] items;

    private float lastSpawn = 0;
    private List<GameObject> spawnedItems = new List<GameObject>();

    void Update()
    {
        if (Time.time - lastSpawn > spawnInterval) {
            lastSpawn = Time.time;
            SpawnItem(null);
        }
    }

    public GameObject SpawnItem(GameObject itemToSpawn)
    {
        if (itemToSpawn != null) {
            GameObject item = Instantiate(itemToSpawn, transform.position, transform.rotation);
            spawnedItems.Add(item);
            return item;
        }
        else if (items[0] != null)
        {
            GameObject item = Instantiate(items[0], transform.position, transform.rotation);
            InteractiveItem itemComponent = item.GetComponent<InteractiveItem>();
            itemComponent.id = "zmrzlina";
            return item;
        }
        return null;
    }
}
