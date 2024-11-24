using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelByName : MonoBehaviour
{
    public string levelName;

    public void LoadLevel()
    {
        FindObjectOfType<LevelManager>().ChangeScene(levelName);
    }
}
