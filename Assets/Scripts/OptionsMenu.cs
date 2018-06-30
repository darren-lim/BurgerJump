using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {


	public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetSensitivity(float sensIndex)
    {
        PlayerPrefs.SetFloat("sensX", sensIndex);
        PlayerPrefs.SetFloat("sensY", sensIndex);
    }
}
