﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemsManager : MonoBehaviour
{
    [SerializeField] ParticleSystem engineSmoke;
    [SerializeField] ParticleSystem engineVapor;
    [SerializeField] ParticleSystem batterySparks;

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


}