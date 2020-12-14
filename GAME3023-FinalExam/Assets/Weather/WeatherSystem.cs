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

    [Header("Weather State")]
    public Weather weather;

    [Header("Audios")]
    public AudioClip[] audioClips;

    [Header("Systems")]
    public SoundSystem soundSystem;
    public RainSystem rainSystem;
    public ThunderSystem thunderSystem;

    [Header("Weather Light")]
    public Light2D globalLight;

    [Header("Game Time")]
    public float GameTime;

    [Header("Global Time Speed")]
    public float timeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        weather = Weather.SUNNY;
        soundSystem = FindObjectOfType<SoundSystem>();
        GameTime = 12;
        timeSpeed = 1;
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

        if(GameTime > 0)
        {
            GameTime -= Time.deltaTime * timeSpeed;
        }
        else
        {
            int randNum;
            switch (weather)
            {
               
                case Weather.SUNNY:
                    weather = Weather.OVERCAST;
                    GameTime = 8;
                    break;
                case Weather.OVERCAST:
                    randNum = Random.Range(1, 3);
                    if (randNum == 1)
                    {
                        weather = Weather.SUNNY;
                        GameTime = 12;
                    }
                    else
                    {
                        weather = Weather.RAINY;
                        GameTime = 6;
                    }
                    break;
                case Weather.RAINY:
                    randNum = Random.Range(1, 3);
                    if(randNum == 1)
                    {
                        weather = Weather.OVERCAST;
                        GameTime = 8;
                    }
                    else
                    {
                        weather = Weather.THUNDERSTORM;
                        GameTime = 12;
                    }
                    break;
                case Weather.THUNDERSTORM:
                    weather = Weather.RAINY;
                    GameTime = 6;
                    break;
            }
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
