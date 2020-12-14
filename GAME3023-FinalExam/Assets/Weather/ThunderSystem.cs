using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ThunderSystem : MonoBehaviour
{
    public Light2D thunder;
    private float timeRemaining, thunderFlicker;
    public bool flashEnabled;
    // Start is called before the first frame update
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
        if (flashEnabled)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
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
            int randNum = Random.Range(1, 5);
            thunder.gameObject.SetActive(false);
            thunderFlicker = 0.2f;
            timeRemaining = randNum;
        }
    }

}
