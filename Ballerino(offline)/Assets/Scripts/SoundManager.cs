using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip goalAmbience;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Shoot()
    {
        audioSource.PlayOneShot(shoot);
    }

    public void GoalAmbience()
    {
        audioSource.PlayOneShot(goalAmbience);
    }
}
