using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public float easeWait;
    public float easeSpeed;
    public HealthGFXController controller;
    private bool easing;
    private float startEase;
    private float lerpFactor;

    public void SetMaxHealth(float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = max;

        easeSlider.maxValue = max;
        easeSlider.value = max;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        //controller.ShowBars();
        if (this.enabled)
        {
            StartCoroutine("EaseBar");
        }
    }


    private void Update()
    {
        if (easing)
        {
            lerpFactor += Time.deltaTime * easeSpeed;
            easeSlider.value = Mathf.Lerp(startEase, healthSlider.value, lerpFactor);
            if (Mathf.Abs(easeSlider.value - healthSlider.value) < 0.01f)
            {
                easing = false;
            }
        }
    }
    private IEnumerator EaseBar()
    {
        startEase = easeSlider.value;
        lerpFactor = 0f;
        yield return new WaitForSeconds(easeWait);
        easing = true;
    }
}
