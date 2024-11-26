using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloadLevel()
    {
        FindObjectOfType<LevelManager>().ChangeScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
