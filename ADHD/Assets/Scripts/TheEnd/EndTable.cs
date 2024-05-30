using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class SimpleTable : MonoBehaviour
{
    private List<string> scenes;
    private List<int> points;
    private List<int> errors;
    private List<int> scores;
    public DatabaseManager DBManager;
    public TextMeshProUGUI outrank;
    public TextMeshProUGUI pointies;
    public TextMeshProUGUI errories;
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI table;

    private void Start()
    {
        List<int> times = new List<int>();
        List<int> justPoints = new List<int>();
        
        GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
        if (globalManagerInstance)
        {
            scenes = globalManagerInstance.GetScenes().ToList();
            scores = globalManagerInstance.GetScores().ToList();
            errors = globalManagerInstance.GetErrors().ToList();
            points = globalManagerInstance.GetPoints().ToList();
            for (int i = 0; i < points.Count; i++)
            {
                string scene = scenes[i];

                switch (scene)
                {
                    case "Concentration":
                        times.Add(points[i]);
                        break;
                    case "Destination":
                        justPoints.Add(points[i]);
                        break;
                    case "Dual-N-back":
                        justPoints.Add(points[i]);
                        break;
                    case "Feeding":
                        justPoints.Add(points[i]);
                        break;
                    case "FlyingSwipe":
                        times.Add(points[i]);
                        break;
                    case "MemoryMatrix":
                        justPoints.Add(points[i]);
                        break;
                    case "SequenceMemory":
                        justPoints.Add(points[i]);
                        break;
                    case "ShadowGame":
                        times.Add(points[i]);
                        break;
                    case "TextColor":
                        justPoints.Add(points[i]);
                        break;
                }
            }
            if (PlayerPrefs.HasKey("PlayerID"))
            {
                int id = PlayerPrefs.GetInt("PlayerID", -1);
                if (id >= 0)
                {
                    DBManager.AddPoints(id, scores, scenes, errors, points);
                    outrank.text = String.Format("You outrank {0:0.00}% of users.", DBManager.GetBetterThanScorePercentage(scores.Sum()) * 100);
                    double scoreavg = DBManager.GetAverageScoreByAge(id);
                    if (scoreavg > scores.Sum())
                    {
                        score.text = String.Format("Your final score is {0}, which is worse than average {3} score in your age group ({1} - {2})", scores.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], scoreavg);
                    }
                    else
                    {
                        score.text = String.Format("Your final score is {0}, which is better than average {3} score in your age group ({1} - {2})", scores.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], scoreavg);
                    }
                    double errorsavg = DBManager.GetAverageErrorByAge(id);
                    if (errorsavg < errors.Sum())
                    {
                        errories.text = String.Format("You made {0} errors, which is worse than average {3} errors in your age group ({1} - {2})", errors.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], errorsavg);
                    }
                    else
                    {
                        errories.text = String.Format("You made {0} errors, which is better than average {3} errors in your age group ({1} - {2})", errors.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], errorsavg);
                    }
                    double timesavg = DBManager.GetAverageErrorByAge(id);
                    if (timesavg < times.Sum())
                    {
                        time.text = String.Format("On games that required speed you took {0} seconds, which is worse than average {3} seconds in your age group ({1} - {2})", times.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], timesavg);
                    }
                    else
                    {
                        time.text = String.Format("On games that required speed you took {0} seconds, which is better than average {3} seconds in your age group ({1} - {2})", times.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], timesavg);
                    }
                    double justpointsavg = DBManager.GetAveragePointByAge(id);
                    if (justpointsavg > justPoints.Sum())
                    {
                        pointies.text = String.Format("On games that required points you scored {0} points, which is worse than average {3} points in your age group ({1} - {2})", justPoints.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], justpointsavg);
                    }
                    else
                    {
                        pointies.text = String.Format("On games that required points you scored {0} points, which is better than average {3} points in your age group ({1} - {2})", justPoints.Sum(), DBManager.AgeLimit(DBManager.GetPlayerAge(id))[0], DBManager.AgeLimit(DBManager.GetPlayerAge(id))[1], justpointsavg);
                    }
                    string tableText = "Scored points for every game: \t";
                    int numRows = Mathf.Min(scenes.Count, scores.Count);
                    for (int row = 0; row < numRows; row++)
                    {
                        tableText += scenes[row] + "\t" + scores[row];

                        if (row < numRows - 1)
                            tableText += "\n";
                    }
                    table.text = tableText;
                }
            }
            Destroy(globalManagerInstance.gameObject);
        }
        
    }

    public void Restart()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        SceneManager.LoadScene(0);
    }
}