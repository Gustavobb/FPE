using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour {
    public AudioMixer mixer;

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("SFXParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MasterParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelAmbientMusic(float sliderValue)
    {
        mixer.SetFloat("AmbientMusicParam", Mathf.Log10(sliderValue) * 20);
    }
}