using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandbyT : MonoBehaviour
{
    public Slider timerSlider;
    public float gameTime;

    private bool stopTimer;
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            float time = gameTime - Time.time;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            //string textTime=string.Format()

            if (time <= 0)
            {
                stopTimer = true;
            }

            if (stopTimer == false)
            {
                timerSlider.value = time;
            }
            print(timerSlider.value);
            print(this.gameObject.name);
        }



    }
}
