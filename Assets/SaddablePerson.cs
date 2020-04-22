using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaddablePerson : MonoBehaviour
{
    [SerializeField] GameObject happyFace;
    [SerializeField] GameObject sadFace;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        happyFace.SetActive(true);
        sadFace.SetActive(false);
    }

    public void MakeSad()
    {
        if (happyFace.activeSelf && audioSource != null)
        {
            Utils.PlayAudio(audioSource, true, 0.1f);
        }
        happyFace.SetActive(false);
        sadFace.SetActive(true);
    }
}
