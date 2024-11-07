using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter whenever something collides with it will check their tag to see if they are the "Player"
    /// If they are a "Player" it will call the Triggered method
    /// </summary>
    /// <param name="other"></param>
   void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Triggered(other);
        }
    }

    /// <summary>
    /// Triggered will change the scene to the next scene based on build index, or back to scene 0 if this is the last scene
    /// </summary>
    /// <param name="other"></param>
    void Triggered(Collider other)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
        Debug.Log("This is the last scene. Return to main menu.");
        SceneManager.LoadScene(0);
        }



    
    }
}
