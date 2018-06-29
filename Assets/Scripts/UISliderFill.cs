using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderFill : MonoBehaviour {

    public Slider slider;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("Volume", 1);
    }
}
