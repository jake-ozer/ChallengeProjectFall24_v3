using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float time = 0;
    private bool canCount = false;

    void Update()
    {
        if (canCount)
        {
            time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
}
