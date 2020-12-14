﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource BGM;

    public void changeBGM(AudioClip music)
    {
        if (BGM.name == music.name)
            return;
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }
}