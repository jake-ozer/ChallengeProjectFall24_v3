using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectComponent : MonoBehaviour
{
    private LevelManager levelManager;
    public string levelName;
    public Image levelRankIcon;
    public bool playable;
    public TextMeshProUGUI buttonTxt;
    public Color activeTextColor;
    public Color inactiveTextColor;
    public GameObject lockIcon;
    public bool isLevel1;
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();
        var bestRank = levelManager.GetBestRank(levelName);
        if (bestRank != null)
        {
            levelRankIcon.gameObject.SetActive(true);
            levelRankIcon.sprite = bestRank.icon;
            timeText.text = Timer.FormattedTime(levelManager.GetBestTime(levelName));
            //timeText.text = convertFloatToMinutes(levelManager.GetBestTime(levelName)).ToString();
        }

        //check with level manager to see if levels are playable
        playable = isLevel1 ? true : levelManager.isPlayable(levelName);
    }

    private void Update()
    {
        if (playable)
        {
            buttonTxt.color = activeTextColor;
            lockIcon.SetActive(false);
            buttonTxt.gameObject.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            buttonTxt.color = inactiveTextColor;
            lockIcon.SetActive(true);
            buttonTxt.gameObject.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public string convertFloatToMinutes(float val)
    {
        TimeSpan time = TimeSpan.FromSeconds(val);
        return time.ToString("mm':'ss");
    }
}
