using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator anim;
    private bool canTrans = true;
 
    public void ChangeScene(string levelName)
    {
        if (canTrans)
        {
            canTrans = false;
            StartCoroutine(FadeTrans(levelName));
        }
    }

    //overload for scene index
    public void ChangeScene(int levelIndex)
    {
        if (canTrans)
        {
            canTrans = false;
            StartCoroutine(FadeTrans(levelIndex));
        }
    }

    private IEnumerator FadeTrans(int levelIndex)
    {
       
        //fade in
        anim.SetTrigger("FadeOut");
        //load scene
        
        yield return new WaitUntil(() => anim.gameObject.GetComponent<Image>().color.a == 1);
        //fade out
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
        anim.SetTrigger("FadeIn");
        canTrans = true;
    }

    private IEnumerator FadeTrans(string levelName)
    {
        //fade in
        anim.SetTrigger("FadeOut");
        //load scene
        
        yield return new WaitUntil(() => anim.gameObject.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(levelName);
        //fade out
        Time.timeScale = 1f;
        anim.SetTrigger("FadeIn");
        canTrans = true;
    }
}
