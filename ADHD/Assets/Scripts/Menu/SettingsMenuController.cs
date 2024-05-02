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

    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVol.value) * 20f);
    }
}