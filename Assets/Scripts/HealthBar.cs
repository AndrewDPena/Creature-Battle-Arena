using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;
    
    public void SetMaxHealth(int health)
        {
            Slider.maxValue = health;
            Slider.value = health;

            Fill.color = Gradient.Evaluate(1f);
        }

    public int GetMaxHealth()
    {
        return (int)Slider.maxValue;
    }
    
    public void SetHealth(int health)
    {
        Slider.value = health;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }

    public int GetHealth()
    {
        return (int) Slider.value;
    }
}
