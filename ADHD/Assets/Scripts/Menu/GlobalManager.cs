using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public List<int> pointsInScene = new List<int>();
    public List<int> errorsInScene = new List<int>();
    public List<int> scoreInScene = new List<int>();
    public List<string> sceneName = new List<string>();
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoints(int point)
    {
        pointsInScene.Add(point);
    }

    public void AddScore(int score)
    {
        scoreInScene.Add(score);
    }

    public void AddError(int error)
    {
        errorsInScene.Add(error);
    }

    public void AddScene(string scene)
    {
        sceneName.Add(scene);
    }

    public List<string> GetScenes()
    {
        return sceneName;
    }

    
    public List<int> GetPoints()
    {
        return pointsInScene;
    }

    public List<int> GetScores()
    {
        return scoreInScene;
    }

    public List<int> GetErrors()
    {
        return errorsInScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
