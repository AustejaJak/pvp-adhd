using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SimpleTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private List<string> scenes;
    private List<int> points;
    private List<int> errors;
    private List<int> scores;
    public DatabaseManager DBManager;

    private void Start()
    {
        GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
        if (globalManagerInstance)
        {
            scenes = globalManagerInstance.GetScenes().ToList();
            scores = globalManagerInstance.GetScores().ToList();
            errors = globalManagerInstance.GetErrors().ToList();
            points = globalManagerInstance.GetPoints().ToList();

            if(PlayerPrefs.HasKey("PlayerID"))
            {
                if(PlayerPrefs.GetInt("PlayerID", -1) >= 0)
                {
                    DBManager.AddPoints(PlayerPrefs.GetInt("PlayerID", -1), scores, scenes, errors, points);
                }
            }
            Destroy(globalManagerInstance.gameObject);

            string tableText = GenerateTable(scenes, scores);
            textMeshProUGUI.text = tableText;
        }
    }

    private string GenerateTable(List<string> column1, List<int> column2)
    {
        string tableText = "";

        int numRows = Mathf.Min(column1.Count, column2.Count);

        for (int row = 0; row < numRows; row++)
        {
            tableText += column1[row] + "\t" + column2[row];

            if (row < numRows - 1)
                tableText += "\n";
        }

        return tableText;
    }

    public void Restart()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene(0);
    }
}