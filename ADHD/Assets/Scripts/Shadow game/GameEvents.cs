using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public GameObject items;
    private List<GameObject> childObjects = new List<GameObject>();
    private List<GameObject> selectedObjects = new List<GameObject>();
    public BasePanel panelScript;
    public int points = 0;
    public string nextSceneName;

    void Start()
    {
        foreach (Transform child in items.transform)
        {
            childObjects.Add(child.gameObject);
        }

        for (int i = 0; i < 8; i++)
        {
            int randomIndex = Random.Range(0, childObjects.Count);

            selectedObjects.Add(childObjects[randomIndex]);

            ClickOnItem clickScript = childObjects[randomIndex].GetComponent<ClickOnItem>();
            clickScript.isHiden = true;
            clickScript.objectId = i;

            childObjects.RemoveAt(randomIndex);
        }

        panelScript.SpawnGrid(selectedObjects);
    }

    public void AddPoint()
    {
        points+=1;
        if(points >= 8) SceneManager.LoadScene(nextSceneName);
    }
}
