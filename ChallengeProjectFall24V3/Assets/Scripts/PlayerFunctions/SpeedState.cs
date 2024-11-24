using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedState : MonoBehaviour
{

    [SerializeField] private float speedChange; //How much you increase in speed per kill
    [SerializeField] private float stateCount;  //How many different speed states there are
    private float currState = 0; //Current Speed State (0 at spawn)
    [SerializeField] private float cooldownTimer;
    private float timer;
    private PlayerMovement playerMvmt;
    [SerializeField] private TextMeshProUGUI speedText;
    private float killCount = 0;

    [SerializeField]
    private Camera myCam;

    private ComboBarManager comboBar;


    private void Awake()
    {
        playerMvmt = FindObjectOfType<PlayerMovement>();
        comboBar = transform.parent.GetChild(1).GetChild(6).GetComponent<ComboBarManager>();
    }


    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed State Manager\nSpeed Level: " + currState.ToString() + "\nSpeed: " + playerMvmt.getSpeed() + "\nTimer: " + (cooldownTimer - Math.Round(timer, 1));
        timer += Time.deltaTime;
        if (timer > cooldownTimer)
        {
            timer = 0;
            
            UpdateSpeedState(false);
        }

        comboBar.SetSlider(cooldownTimer, timer);
    }

    public void UpdateSpeedState(bool raiseSpeed)
    {
       
        if (raiseSpeed == false) //Speed down
        {
            comboBar.ComboReset();
            if(this.currState != 0) //If speed state isn't 0 (lowest)
            {
                playerMvmt.updateSpeed(-1);
                //this.currState--;
                this.currState = 0;
                //Debug.Log("Speed Down!!");
                playerMvmt.updateTargetFOV(-1);
            }


        }else//Speed up
        {
            comboBar.ComboIncrease();
            var rankManager = FindObjectOfType<RankManager>();
            if(rankManager != null)
            {
                rankManager.addKill(1);
            }
                
            if (this.currState < stateCount) //If speed state isn't stateCount (highest state)
            {
                //Debug.Log("Speed Up!!");
                playerMvmt.updateSpeed(speedChange);
                this.currState++;
                if (currState % 3 == 0)
                {
                    playerMvmt.updateTargetFOV(5);
                }
            }


        }
        timer = 0;

    }

    

}
