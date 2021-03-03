using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timer;
    private float startTime;
    public GameController gameController;
    public float time;
    void Start()
    {

        time = GameController.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        setTime();
    }

    private void setTime()
    {

        time = GameController.time;

        string mins = ((int)time / 60).ToString();
        string secs = (time % 60).ToString("f0");
        timer.text = mins + ":" + secs;

    }
    
}
