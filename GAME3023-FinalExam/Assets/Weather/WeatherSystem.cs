using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum Weather
{
    SUNNY,
    OVERCAST,
    RAINY,
    THUNDERSTORM
}
public class WeatherSystem : MonoBehaviour
{

    public Weather weather;

    public AudioClip[] audioClips;

    [Header("Systems")]
    public SoundSystem soundSystem;
    public RainSystem rainSystem;
    public ThunderSystem thunderSystem;

    [Header("Weather Light")]
    public Light2D globalLight;

    // Start is called before the first frame update
    void Start()
    {
        weather = Weather.SUNNY;
        soundSystem = FindObjectOfType<SoundSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(weather)
        {
            case Weather.SUNNY:
                changeMusic(audioClips[0]);
                rainSystem.StopRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(210, 0, 0, 0.1f);
                break;
            case Weather.RAINY:
                changeMusic(audioClips[1]);
                rainSystem.PlayRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            case Weather.THUNDERSTORM:
                changeMusic(audioClips[2]);
                rainSystem.PlayRain();
                thunderSystem.flashEnabled = true;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            case Weather.OVERCAST:
                soundSystem.StopBGM();
                rainSystem.StopRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;

        }
    }

    void changeMusic(AudioClip audioClip)
    {
        if (soundSystem.BGM.isPlaying && soundSystem.BGM.clip.name == audioClip.name)
        {
            return;
        }
        else
        {
            soundSystem.changeBGM(audioClip);
        }
    }
}
