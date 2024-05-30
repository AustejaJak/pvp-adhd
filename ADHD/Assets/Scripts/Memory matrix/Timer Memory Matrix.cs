using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerMemoryMatrix : MonoBehaviour
{
    public float totalTime = 60f;
    private float timer;
    [SerializeField] private TextMeshProUGUI timeLabel;

    void Start()
    {
        timer = totalTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            MemoryMatrix memoryMatrixInstance = FindObjectOfType<MemoryMatrix>();
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(globalManagerInstance && memoryMatrixInstance)
            {
                globalManagerInstance.AddScore((int)((memoryMatrixInstance.GetPoints()-(0.5*memoryMatrixInstance.GetErrors()))*(10.0/memoryMatrixInstance.GetPoints())*10));
                globalManagerInstance.AddPoints(memoryMatrixInstance.GetPoints());
                globalManagerInstance.AddError(memoryMatrixInstance.GetErrors());
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void UpdateTimerUI()
    {
        timeLabel.text = " " + Mathf.Round(timer);
    }
}
