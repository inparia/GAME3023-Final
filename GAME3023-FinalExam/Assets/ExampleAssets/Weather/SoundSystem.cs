using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource BGM;

    public void changeBGM(AudioClip music)
    {
        //If given bgm is already playing, don't play it again
        if (BGM.name == music.name)
            return;
        //Set BGM to new given music.
        BGM.Stop();
        BGM.clip = music;
        //BGM Fade in
        StartCoroutine(AudioFade.FadeIn(BGM, 1.0f));
    }

    // Stop BGM
    public void StopBGM()
    {
        BGM.Stop();
    }
}
