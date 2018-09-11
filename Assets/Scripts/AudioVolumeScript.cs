using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeScript : MonoBehaviour {

    public AudioMixer audioGroup;
    public AudioSource music;

	void Start ()
    {
        music = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        audioGroup.SetFloat("Master", PlayerPrefs.GetFloat("Volume", 0f));
	}

    public void setMasterVolume(float volume)
    {
        float newVolume = volume * 80;
        audioGroup.SetFloat("Master", newVolume);
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
