using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float windForceX = 0.0f;
    public GameObject inventoryContainer;
    public bool testing = false; //TOREMOVE
    public float spawnInterval = 4.0f; //TOREMOVE
    public GameObject[] items; //TOREMOVE

    float lastSpawn = 0;
    List<GameObject> spawnedItems = new List<GameObject>();

    void Update()
    {
        if (testing && Time.time - lastSpawn > spawnInterval) { //TOREMOVE
            lastSpawn = Time.time;
            SpawnItem(null, 0);
        }

        List<GameObject> itemsToDestroy = new List<GameObject>();
        float despawnY = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y;
        foreach(GameObject spawnedItem in spawnedItems)
        {
            if (spawnedItem == null || spawnedItem.transform.position.y < despawnY) {
                itemsToDestroy.Add(spawnedItem);
            }
        }
        foreach(GameObject itemToDestroy in itemsToDestroy)
        {
            spawnedItems.Remove(itemToDestroy);
            Destroy(itemToDestroy);
        }
    }

    void FixedUpdate()
    {
        foreach(GameObject spawnedItem in spawnedItems)
        {
            if (spawnedItem != null && windForceX != 0)
            {
                Debug.Log("err");
                Rigidbody2D itemRb2D = spawnedItem.GetComponent<Rigidbody2D>();
                itemRb2D.AddForce(new Vector2(windForceX, 0));
            }
        }
        
    }

    public GameObject SpawnItem(GameObject itemToSpawn, int index)
    {
        if (itemToSpawn != null) {
            GameObject item = Instantiate(itemToSpawn, transform.position+Vector3.up*(index+2)*0.8f, transform.rotation);
            spawnedItems.Add(item);
            return item;
        }
        else if (testing && items.Length > 0) //TOREMOVE
        {
            GameObject item = Instantiate(items[0], transform.position, transform.rotation);
            InteractiveItem itemComponent = item.GetComponent<InteractiveItem>();
            itemComponent.itemType = ItemType.ICECREAM;
            spawnedItems.Add(item);
            return item;
        }
        return null;
    }
}
