using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeScript : MonoBehaviour {

    public AudioMixer audioGroup;
    public AudioSource music;
    
    private void Start()
    {
        music = GetComponent<AudioSource>();
        float initVolume = PlayerPrefs.GetFloat("Volume", 1f);
        audioGroup.SetFloat("Master", Mathf.Log(initVolume) * 20);
    }

    public void setMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        audioGroup.SetFloat("Master", Mathf.Log(volume) * 20);
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
