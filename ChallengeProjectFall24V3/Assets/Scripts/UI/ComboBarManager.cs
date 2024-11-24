using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboBarManager : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [SerializeField] int startColorIndex;

    private UIGradient gradient;
    private Image image;
    private Slider slider;

    private int comboNum;
    private int color1;
    private int color2;
    [SerializeField] private float sliderValue;

    [SerializeField] private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        sliderValue = 0;
        gradient = transform.GetChild(1).GetChild(0).GetComponent<UIGradient>();
        image = transform.GetChild(3).GetComponent<Image>();
        slider = GetComponent<Slider>();
        ComboReset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ResetGradient()
    {
        color1 = startColorIndex;
        color2 = ColorIncrease(color1);
    }

    public void ComboIncrease()
    {
        comboNum++;

        text.text = comboNum.ToString();

        if(comboNum != 1)
        {
            color1 = ColorIncrease(color1);
            color2 = ColorIncrease(color2);
            ChangeGradient();
        }

    }

    public void ComboReset()
    {
        comboNum = 0;

        text.text = comboNum.ToString();

        ResetGradient();
        ChangeGradient();
    }

    private int ColorIncrease(int color)
    {
        if (color + 1 == colors.Length)
        {
            return 0;
        }
        return color + 1;

    }

    private void ChangeGradient()
    {
        gradient.NewGradient(colors[color1], colors[color2]);
        image.color = colors[color1];
    }

    public void SetSlider(float coolDownTimer, float timer)
    {
        if (comboNum != 0)
        {
            sliderValue = (coolDownTimer - timer) / coolDownTimer;
        }
        else
        {
            sliderValue = 0;
        }
        slider.value = sliderValue;

    }
}
