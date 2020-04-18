using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float spawnInterval = 4.0f;
    public GameObject[] items; //TOREMOVE

    float lastSpawn = 0;
    List<GameObject> spawnedItems = new List<GameObject>();

    void Update()
    {
        if (Time.time - lastSpawn > spawnInterval) { //TOREMOVE
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
        else if (items[0] != null) //TOREMOVE
        {
            GameObject item = Instantiate(items[0], transform.position, transform.rotation);
            InteractiveItem itemComponent = item.GetComponent<InteractiveItem>();
            itemComponent.id = "zmrzlina";
            return item;
        }
        return null;
    }
}
