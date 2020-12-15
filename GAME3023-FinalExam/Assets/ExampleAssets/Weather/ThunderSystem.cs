using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ThunderSystem : MonoBehaviour
{
    [Header("Thunder Effect")]
    public Light2D thunder;
    public bool flashEnabled;

    private float timeRemaining, thunderFlicker;
    // Start is called before the first frame update Initializing all variables.
    void Start()
    {
        int randNum = Random.Range(1, 5);
        timeRemaining = randNum;
        thunderFlicker = 0.2f;
        flashEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Time for the thunder to prepare for flash. (Changes every time)
        if (flashEnabled)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Time for Thunder Flickering.
                if(thunderFlicker >0)
                {
                    thunder.gameObject.SetActive(true);
                    thunderFlicker -= Time.deltaTime;
                }
                else
                {
                    int randNum = Random.Range(1, 5);
                    thunder.gameObject.SetActive(false);
                    thunderFlicker = 0.2f;
                    timeRemaining = randNum;
                }
                
            }
        }
        else
        {
            //If flash is not enabled by the weather system (or weather is not set to thunderstorm) then don't start the timer.
            int randNum = Random.Range(1, 5);
            thunder.gameObject.SetActive(false);
            thunderFlicker = 0.2f;
            timeRemaining = randNum;
        }
    }

}
