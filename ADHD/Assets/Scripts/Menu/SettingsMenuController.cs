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

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}