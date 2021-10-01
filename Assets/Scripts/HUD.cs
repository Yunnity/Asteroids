using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text timeAlive;
    string preText = "Time Alive: ";
    string postText = " s";
    float secondsElapsed = 0;
    public static bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive.text = preText + secondsElapsed.ToString() + postText;
    }

    // Update is called once per frame
    void Update()
    {
        if(running)
        {
            secondsElapsed += Time.deltaTime;
            timeAlive.text = preText + ((int)(secondsElapsed)).ToString() + postText;
        }
    }
}
