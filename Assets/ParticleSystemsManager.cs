using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemsManager : MonoBehaviour
{
    [SerializeField] ParticleSystem engineSmoke;
    [SerializeField] ParticleSystem engineVapor;
    [SerializeField] ParticleSystem batterySparks;
    [SerializeField] ParticleSystem bubbles;
    [SerializeField] ParticleSystem dirt;
    [SerializeField] Animator wrench;
    [SerializeField] Animator gas;
    [SerializeField] SpriteRenderer icecream;
    [SerializeField] AudioClip soundBubbles;
    [SerializeField] AudioClip soundElectricity;
    [SerializeField] AudioClip soundIcecream;

    int wrenches = 0;
    int refilling = 0;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartOverheating()
    {
        engineSmoke.Play();
    }

    public void CoolDown()
    {
        engineSmoke.Stop();
    }

    public void ApplyIcecream()
    {
        audioSource.PlayOneShot(soundIcecream);
        engineVapor.Play();
        icecream.gameObject.SetActive(true);
        icecream.color = Color.white;
        Utils.tweenColor(icecream, Color.clear, 1, 1);
    }

    public void ChargeBattery()
    {
        audioSource.PlayOneShot(soundElectricity);
        batterySparks.Play();
    }

    public void RefillWater()
    {
        audioSource.PlayOneShot(soundBubbles);
        bubbles.Play();
    }

    public void EnterDirt(Vector3 position)
    {
        dirt.transform.position = position;
        dirt.Play();
    }

    public void Repair()
    {
        wrenches++;
        wrench.gameObject.SetActive(true);
        StartCoroutine(StopWrenchRepair());
    }

    IEnumerator StopWrenchRepair()
    {
        yield return new WaitForSeconds(2);
        wrenches--;
        if (wrenches == 0)
        {
            wrench.gameObject.SetActive(false);
        }
    }

    public void RefillGass()
    {
        refilling++;
        gas.gameObject.SetActive(true);
        gas.Play("GasRefill");
        StartCoroutine(StopGasRefill());
    }

    IEnumerator StopGasRefill()
    {
        yield return new WaitForSeconds(2);
        refilling--;
        if (refilling == 0)
        {
            gas.gameObject.SetActive(false);
        }
    }

}
