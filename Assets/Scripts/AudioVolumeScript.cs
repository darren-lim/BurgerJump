using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeScript : MonoBehaviour {

    public AudioMixer audioGroup;
    public AudioSource music;
    //public Slider slider;

	// Use this for initialization
	void Start ()
    {
        //music.Play();
        audioGroup.SetFloat("Master", PlayerPrefs.GetFloat("Volume", 1));
        //slider.value = PlayerPrefs.GetFloat("Volume", 1);
	}

    public void setMasterVolume(float volume)
    {
        audioGroup.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void playMusic()
    {
        music.Play();
    }
    public void stopMusic()
    {
        music.Stop();
    }
    public void pauseMusic()
    {
        music.Pause();
    }
    public void unpauseMusic()
    {
        music.UnPause();
    }
}
