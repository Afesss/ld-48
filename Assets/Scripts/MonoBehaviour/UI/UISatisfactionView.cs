﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISatisfactionView : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetValue(float value)
    {
        slider.value = value;
    }
}