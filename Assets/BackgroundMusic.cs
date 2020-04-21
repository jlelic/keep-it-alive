using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{


    public static BackgroundMusic Instance { get; private set; }

    [SerializeField] AudioClip clipNormal;
    [SerializeField] AudioClip clipFast;
    AudioSource source;
    bool isFast;
    float initialVolume;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clipNormal;
        source.Play();
        source.loop = true;
        initialVolume = source.volume;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(!isFast && GameManager.Instance.Progress > 500)
        {
            isFast = true;
            StartCoroutine(SwitchClip(clipFast));
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(isFast)
        {
            StartCoroutine(SwitchClip(clipNormal));
            isFast = false;
        }
    }

    IEnumerator SwitchClip(AudioClip clip)
    {
        for (int i = 0; i <= 20; i++)
        {
            yield return new WaitForSeconds(0.1f);
            source.volume = ((20 - i) / 20f) * initialVolume;
        }
        source.Stop();
        source.clip = clip;
        source.volume = initialVolume;
        source.Play();
    }
}
