using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    [Header("Rain Effect")]
    public ParticleSystem rain;
    
    //Play rain if weather is set to rainy.
    public void PlayRain()
    {
        //if rain is already playing, then don't play again.
        if (rain.isPlaying)
            return;
        rain.Play();
    }

    //Stop rain if weather is not set to rainy.
    public void StopRain()
    {   
        rain.Stop();
    }
}
