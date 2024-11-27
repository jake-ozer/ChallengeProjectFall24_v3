using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NewSceneTrigger : MonoBehaviour
{
    private RankManager rankMng;

    private void Start()
    {
        rankMng = FindObjectOfType<RankManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rankMng.setEndLevel(true);
            Time.timeScale = 0f;
            FindObjectOfType<PlayerShoot>().enabled = false;
            FindObjectOfType<PlayerSlide>().enabled = false;
            FindObjectOfType<PlayerCamera>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    
}
