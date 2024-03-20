using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLabel;
    private float sceneTime = 0f;
    public float SceneTime => sceneTime;
    private GlobalManager globalManager;

    void Start()
    {
        globalManager = FindAnyObjectByType<GlobalManager>();
    }

    void Update()
    {
        globalManager.totalSceneTime += Time.deltaTime;
        sceneTime += Time.deltaTime;
        timeLabel.text = TimeSpan.FromSeconds(sceneTime).ToString("ss");
    }
}
