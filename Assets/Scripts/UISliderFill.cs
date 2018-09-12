using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderFill : MonoBehaviour {

    public Slider VolumeSlider;
    public Slider SensSlider;

    private void OnEnable()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SensSlider.value = PlayerPrefs.GetFloat("localSens", 6);
    }
}
