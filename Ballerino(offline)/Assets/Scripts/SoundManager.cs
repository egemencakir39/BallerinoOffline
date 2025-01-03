using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip goalAmbience;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip endSound;

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

    public void StartSound()
    {
        audioSource.PlayOneShot(startSound);
    }

    public void EndSound()
    {
        audioSource.PlayOneShot(endSound);
    }
}
