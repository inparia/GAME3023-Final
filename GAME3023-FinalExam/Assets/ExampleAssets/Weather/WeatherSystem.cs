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
    private RainSystem rainSystem;
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
        weather = Weather.SUNNY;
        soundSystem = FindObjectOfType<SoundSystem>();
        rainSystem = FindObjectOfType<RainSystem>();
        thunderSystem = FindObjectOfType<ThunderSystem>();
        clipSystem = FindObjectOfType<ClipSystem>();
        setGameTime(12);
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
                rainSystem.StopRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(210, 0, 0, 0.1f);
                break;
            case Weather.RAINY:
                changeMusic(clipSystem.GetClip("Rain"));
                rainSystem.PlayRain();
                thunderSystem.flashEnabled = false;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            case Weather.THUNDERSTORM:
                changeMusic(clipSystem.GetClip("Thundering"));
                rainSystem.PlayRain();
                thunderSystem.flashEnabled = true;
                globalLight.color = new Color(0, 0, 186, 0.1f);
                break;
            // For overcast, stop the bgm for quiet environment
            case Weather.OVERCAST:
                soundSystem.StopBGM();
                rainSystem.StopRain();
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
                    weather = Weather.OVERCAST;
                    setGameTime(8);
                    break;
                case Weather.OVERCAST:
                    // Random number of 1 to 2
                    randNum = Random.Range(1, 3);
                    if (randNum == 1)
                    {
                        weather = Weather.SUNNY;
                        setGameTime(12);
                    }
                    else
                    {
                        weather = Weather.RAINY;
                        setGameTime(6);
                    }
                    break;
                case Weather.RAINY:
                    // Random number of 1 to 2
                    randNum = Random.Range(1, 3);
                    if(randNum == 1)
                    {
                        weather = Weather.OVERCAST;
                        setGameTime(8);
                    }
                    else
                    {
                        weather = Weather.THUNDERSTORM;
                        setGameTime(12);
                    }
                    break;
                //Thunderstorm will set weather back to rainy.
                case Weather.THUNDERSTORM:
                    weather = Weather.RAINY;
                    setGameTime(6);
                    break;
            }
        }
    }

    void setGameTime(int gameTime)
    {
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
