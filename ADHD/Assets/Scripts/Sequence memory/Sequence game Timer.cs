using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequencegameTimer : MonoBehaviour
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
            Sequencegame sequenceGame = FindObjectOfType<Sequencegame>();
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(globalManagerInstance && sequenceGame)
            {
                int points = sequenceGame.GetPoints();
                int errors = sequenceGame.GetErrors();
                globalManagerInstance.AddScore((int)(points*(10.0/15.0)*10));
                globalManagerInstance.AddPoints(points);
                globalManagerInstance.AddError(errors);
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
