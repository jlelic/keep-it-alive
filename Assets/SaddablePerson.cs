using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaddablePerson : MonoBehaviour
{
    [SerializeField] GameObject happyFace;
    [SerializeField] GameObject sadFace;

    private void Start()
    {
        happyFace.SetActive(true);
        sadFace.SetActive(false);
    }

    public void MakeSad()
    {
        happyFace.SetActive(false);
        sadFace.SetActive(true);
    }
}
