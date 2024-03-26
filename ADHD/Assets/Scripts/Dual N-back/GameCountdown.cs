using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameCountdown : MonoBehaviour
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
            DualNBackController dualNBackInstance = FindObjectOfType<DualNBackController>();
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(globalManagerInstance && dualNBackInstance)
            {
                globalManagerInstance.AddPoints(dualNBackInstance.GetScore());
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void UpdateTimerUI()
    {
        timeLabel.text = "Time: " + Mathf.Round(timer);
    }
}
