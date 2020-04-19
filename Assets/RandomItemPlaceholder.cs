using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemPlaceholder : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs;

    void Start()
    {
        var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        var newObject = Instantiate(prefab);
        newObject.transform.SetParent(transform.parent);
        newObject.transform.position = transform.position;
        Destroy(gameObject);
    }
}
