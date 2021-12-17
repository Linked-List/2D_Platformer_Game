using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXList : MonoBehaviour
{
    AudioSource myAudio;

    public AudioClip[] sounds;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayerSFX_SoundPlay(int SoundNumber)
    {
        myAudio.clip = sounds[SoundNumber];

        myAudio.Play();

    }
}
