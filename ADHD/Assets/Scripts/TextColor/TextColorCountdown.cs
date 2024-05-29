using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class TextColorCountdown : MonoBehaviour
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
            TextColor textColorInstance = FindObjectOfType<TextColor>();
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(globalManagerInstance && textColorInstance)
            {
                int points = textColorInstance.GetPoints();
                int errors = textColorInstance.GetErrors();
                globalManagerInstance.AddScore((int)((points-(0.5*errors))*(10.0/40.0)));
                globalManagerInstance.AddPoints(points);
                globalManagerInstance.AddError(errors);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void UpdateTimerUI()
    {
        timeLabel.text = "" + Mathf.Round(timer);
    }
}
