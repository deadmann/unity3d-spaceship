using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private Image _fill;

    public Gradient gradient;

    public void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
        _fill = transform.Find("Fill").GetComponent<Image>();
    }

    private void Start()
    {
    }

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;

        _fill.color = gradient.Evaluate(1f);
    }
    
    public void SetHealth(int health)
    {
        _slider.value = health;

        _fill.color = gradient.Evaluate(_slider.normalizedValue);
    }
}
