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

    int wrenches = 0;

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
        engineVapor.Play();
    }

    public void ChargeBattery()
    {
        batterySparks.Play();
    }

    public void RefillWater()
    {
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
        if(wrenches == 0)
        {
            wrench.gameObject.SetActive(false);
        }
    }

}
