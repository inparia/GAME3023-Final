using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//States for weathers
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

    //[Header("Systems")]
    private SoundSystem soundSystem;
    private EffectSystem effectSystem;
    private ThunderSystem thunderSystem;
    private ClipSystem clipSystem;

    [Header("Weather Light")]
    public Light2D globalLight;

    //[Header("Game Time")]
    private float GameTime;

    [Header("Global Time Speed")]
    public float timeSpeed;

    // Start is called before the first frame update. Initializing the weather state, gametime, and time speed.
    void Start()
    {
        soundSystem = FindObjectOfType<SoundSystem>();
        effectSystem = FindObjectOfType<EffectSystem>();
        thunderSystem = FindObjectOfType<ThunderSystem>();
        clipSystem = FindObjectOfType<ClipSystem>();
        setWeatherGameTime(Weather.SUNNY, 12);
        timeSpeed = 1;
    }


    // Update is called once per frame
    void Update()
    {
        //Check which weather state it currently is at and set the weather colour, bgm based on the state. If the state is on thunderstorm, then there will be light flashing
        switch(weather)
        {
            case Weather.SUNNY:
                changeMusic(clipSystem.GetClip("Sunny"));
                effectSystem.StopRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(210, 0, 0, 0.1f);
                break;
            case Weather.RAINY:
                changeMusic(clipSystem.GetClip("Rain"));
                effectSystem.PlayRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            case Weather.THUNDERSTORM:
                changeMusic(clipSystem.GetClip("Thundering"));
                effectSystem.PlayRain();
                thunderSystem.flashEnabled = true;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            // For overcast, stop the bgm for quiet environment
            case Weather.OVERCAST:
                soundSystem.StopBGM();
                effectSystem.StopRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;

        }

        // Timer for the game. Each states have or can be set to different game time.
        if(GameTime > 0)
        {
            GameTime -= Time.deltaTime * timeSpeed;
        }
        else
        {
            // For Overcast, and Rainy, it has randomized state change so by using random, its weather state change accordingly.
            int randNum;
            switch (weather)
            {
                //Sunny will set weather to overcast.
                case Weather.SUNNY:
                    setWeatherGameTime(Weather.OVERCAST, 8);
                    break;
                case Weather.OVERCAST:
                    // Random number of 1 to 2
                    randNum = Random.Range(1, 3);
                    switch(randNum)
                    {
                        case 1:
                            setWeatherGameTime(Weather.SUNNY, 12);
                            break;
                        case 2:
                            setWeatherGameTime(Weather.RAINY, 6);
                            break;
                    }
                    break;
                case Weather.RAINY:
                    // Random number of 1 to 2
                    randNum = Random.Range(1, 3);
                    switch(randNum)
                    {
                        case 1:
                            setWeatherGameTime(Weather.OVERCAST, 8);
                            break;
                        case 2:
                            setWeatherGameTime(Weather.THUNDERSTORM, 12);
                            break;
                    }
                    break;
                //Thunderstorm will set weather back to rainy.
                case Weather.THUNDERSTORM:
                    setWeatherGameTime(Weather.RAINY, 6);
                    break;
            }
        }
    }

    void setWeatherGameTime(Weather pweather, int gameTime)
    {
        weather = pweather;
        GameTime = gameTime;
    }
    // Change bgm of the game by given audio clip.
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
