using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public float totalSceneTime = 0f;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        totalSceneTime = 0f;
        Debug.Log("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
