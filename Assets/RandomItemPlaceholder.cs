using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemPlaceholder : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs;

    void Start()
    {
        if (itemPrefabs.Length == 0)
        {
            return;
        }
        var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        if(prefab != null)
        {
            var newObject = Instantiate(prefab);
            newObject.transform.SetParent(transform.parent);
            newObject.transform.localScale = transform.localScale;
            newObject.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
