using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLabel;
    private float sceneTime = 0f;

    void Start()
    {
    }

    void Update()
    {
        sceneTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(sceneTime);
        timeLabel.text = string.Format("{0:D2}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
    }

    public int GetTotalSeconds()
    {
        return (int)sceneTime;
    }
}
