using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RankCanvasUIControl : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject endMenu;
    private RankManager rankManager;
    public GameObject newBestAttemptMenu;

    //Start Menu GameObjects
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI bronzeBenchmarkTime;
    public TextMeshProUGUI silverBenchmarkTime;
    public TextMeshProUGUI goldBenchmarkTime;
    public TextMeshProUGUI platBenchmarkTime;
    public TextMeshProUGUI bestTime;
    public Image rankDisplay;
    
    //End Menu GameObjects
    //Current Attempt Info
    public TextMeshProUGUI timeAchieved;
    public Image rankAchieved;
    //Best Time Achieved
    public Image bestRankAchieved;
    public TextMeshProUGUI bestTimeAchieved;
   
    //NEW HIGH SCORE 
    public TextMeshProUGUI prevTime;
    public Image prevRank;

    private MainInput input;

    public AudioClip endMenuSound;

    private void Awake()
    {
        input = new MainInput();
    }

    // --------- enable/disbale input when script toggled on/off -------------- 
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    // ------------------------Do not touch this------------------------------


    private void Start() //Starts on every scene.
    {
        rankManager = FindObjectOfType<RankManager>();
        levelName.text = rankManager.levelName;
        bool hasPlayed = LevelManager.instance.HasPlayed(rankManager.levelName);
        //bronzeBenchmarkTime.text = convertFloatToMinutes(rankManager.bronzeTimeSeconds);  you just need to finish level in all cases
        silverBenchmarkTime.text = convertFloatToMinutes(rankManager.silverTimeSeconds);
        goldBenchmarkTime.text = convertFloatToMinutes(rankManager.goldTimeSeconds);
        platBenchmarkTime.text = convertFloatToMinutes(rankManager.platTimeSeconds);
        bestTime.text = "0s";


        if(hasPlayed)
        {
            if (!rankDisplay.gameObject.activeSelf)
            {
                rankDisplay.gameObject.SetActive(true);
            }
            rankDisplay.sprite = LevelManager.instance.GetBestRank(rankManager.levelName).icon;
            bestTime.text = convertFloatToMinutes(LevelManager.instance.GetBestTime(rankManager.levelName));
            
        }

        startMenu.SetActive(true);
    }

    bool once3 = true;
    public void EnableEndMenu(bool newHighScore)
    {
        if(once3)
        {
            GetComponent<AudioSource>().PlayOneShot(endMenuSound);
            once3 = false;
        }
        
        timeAchieved.text = convertFloatToMinutes(rankManager.timer);
        rankAchieved.sprite = rankManager.getRank(rankManager.timer).icon;
        rankAchieved.gameObject.SetActive(true);
        bestTimeAchieved.text = convertFloatToMinutes(LevelManager.instance.GetBestTime(rankManager.levelName));
        if (LevelManager.instance.GetBestRank(rankManager.levelName) != null)
        {
            bestRankAchieved.gameObject.SetActive(true);
            bestRankAchieved.sprite = LevelManager.instance.GetBestRank(rankManager.levelName).icon;
        }
        
        

        //Extra guidelines to make sure that current attempt has atleast 1 previous attempt before.
        //rM.prevTime and rM.prevRank are initalized to 999f and null, respectively in RankManager script. 
        
       /* if (newHighScore && rankManager.prevTime != 0f && rankManager.prevRank != null) 
        {
            prevTime.text = convertFloatToMinutes(rankManager.prevTime);
            prevRank.sprite = rankManager.prevRank.icon;
            newBestAttemptMenu.SetActive(true);
        } else if(newHighScore && rankManager.prevTime == 0f && rankManager.prevRank == null)
        {
            //If these inputs haven't changed, then that means this is user's first time so it should display 0 seconds/no prev rank.
            prevTime.text = "0s";
            newBestAttemptMenu.SetActive(true);
        }*/


        endMenu.SetActive(true);
        GetComponent<Animator>().SetTrigger("ShowEndMenu");
    }

    public string convertFloatToMinutes(float val)
    {
        //TimeSpan time = TimeSpan.FromSeconds(val);
        //return time.ToString("mm':'ss"); 
        int minutes = Mathf.FloorToInt(val / 60f);
        int seconds = Mathf.FloorToInt(val % 60f);
        int milliseconds = Mathf.FloorToInt((val * 1000) % 1000 / 10);

        //format string as mm:ss.mmm
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    bool once = true;
    private void Update()
    {
        //if player moves at all, get rid of startmenu anim
        bool moved;
        moved = input.Ground.Move.ReadValue<Vector2>() != Vector2.zero || input.Ground.Jump.triggered || Keyboard.current.anyKey.wasPressedThisFrame || input.Ground.Shoot.triggered;
        if (moved)
        {
            GetComponent<Animator>().SetTrigger("StartMenuFade");
            FindObjectOfType<Timer>().StartTimer();
            if (once)
            {
                Invoke("MakeStartMenuInactive", 5f);
                once = false;
            }
           
        }
    }
    private void MakeStartMenuInactive()
    {
        startMenu.SetActive(false);
        GameObject.Find("Parentforstarttext").SetActive(false);
    }
}
