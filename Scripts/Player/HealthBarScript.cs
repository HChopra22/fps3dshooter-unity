using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A script to adjust slider fill depending on the player/AI health
public class HealthBarScript : MonoBehaviour
{
    [Header("Slider Graphics")]
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //when the health is at max, the slider is at max and the gradient is at 1
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //the value of the slider changes with health and the gradient changes the fill colour accordingly
   public void setHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
