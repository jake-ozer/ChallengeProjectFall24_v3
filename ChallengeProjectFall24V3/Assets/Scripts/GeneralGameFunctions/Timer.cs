using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float time = 0;
    private bool canCount = false;
    private float updateInterval = 0.025f;
    private float lastUpdate = 0f;

    void Update()
    {
        if (canCount)
        {
            time += Time.deltaTime;
            
            if (time - lastUpdate >= updateInterval)
            {
                timerText.text = FormattedTime(time);
                lastUpdate = time;
            }
            
        }

        //Debug.Log(Cursor.lockState);
    }

    public void StartTimer()
    {
        canCount = true;
    }

    public float GetTime()
    {
        return time;
    }

    public static string FormattedTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f); 
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000) % 1000 / 10);

        //format string as mm:ss.mmm
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}
