using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {
    
    
    public AudioClip jumpClip;
    public AudioClip powerJumpClip;
    public AudioClip explodeClip;
    private AudioSource audioSource;
    
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Rocket.onRocketJumped += Rocket_OnRocketJumped;
        Rocket.onRocketExploded += Rocket_OnRocketExploded;
        Rocket.onRocketPowerJumped += Rocket_OnRocketPowerJumped;
    }


    private void OnDestroy()
    {
        Rocket.onRocketJumped -= Rocket_OnRocketJumped;
        Rocket.onRocketExploded -= Rocket_OnRocketExploded;
        Rocket.onRocketPowerJumped -= Rocket_OnRocketPowerJumped;
    }

    
    private void Rocket_OnRocketJumped()
    {
        audioSource.PlayOneShot(jumpClip);
    }
    
    private void Rocket_OnRocketPowerJumped()
    {
        audioSource.PlayOneShot(powerJumpClip);
    }

    private void Rocket_OnRocketExploded()
    {
        audioSource.PlayOneShot(explodeClip);
    }



    

    


}
