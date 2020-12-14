using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{

    public ParticleSystem rain;

    public void PlayRain()
    {
        if (rain.isPlaying)
            return;
        rain.Play();
    }

    public void StopRain()
    {
        rain.Stop();
    }
}
