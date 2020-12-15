using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    [Header("Rain Effect")]
    public ParticleSystem[] particles;
    
    //Play rain if weather is set to rainy.
    public void PlayRain()
    {
        //if rain is already playing, then don't play again.
        if (particles[0].isPlaying)
            return;
        particles[0].Play();
    }

    //Stop rain if weather is not set to rainy.
    public void StopRain()
    {
        particles[0].Stop();
    }
}
