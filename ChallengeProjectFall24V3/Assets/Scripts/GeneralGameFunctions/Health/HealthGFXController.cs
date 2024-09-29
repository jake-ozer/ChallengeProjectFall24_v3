using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGFXController : MonoBehaviour
{

    public bool showBars;
    public GameObject healthBar;
    public GameObject easeBar;
    public float showTime;

    private void Awake()
    {
        showBars = true;
    }

    private void Update()
    {
        if (showBars)
        {
            healthBar.SetActive(true);
            easeBar.SetActive(true);
        }
        else
        {
            healthBar.SetActive(false);
            easeBar.SetActive(false);
        }
    }

    public void ShowBars()
    {
        StopAllCoroutines();
        StartCoroutine("ShowBarsAfterTime");
    }

    private IEnumerator ShowBarsAfterTime()
    {
        showBars=true;
        yield return new WaitForSeconds(showTime);
        showBars=false;
    }
}
