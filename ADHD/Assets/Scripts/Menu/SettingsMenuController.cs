using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    public Slider masterVol;
    public AudioMixer audioMixer;

    private void Start()
    {
        // Load the master volume setting
        masterVol.value = PlayerPrefs.GetFloat("MasterVol", 1f);
    }

    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVol.value) * 20f);
        PlayerPrefs.SetFloat("MasterVol", masterVol.value);
        AudioManager.instance.LoadMasterVolume();
    }
}