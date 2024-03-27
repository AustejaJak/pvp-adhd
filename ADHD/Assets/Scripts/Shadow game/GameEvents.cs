using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int missclicks = 0;
    [SerializeField] private TextMeshProUGUI missclickLabel;

    void Start()
    {
        missclickLabel.text = "Missclicks = " + missclicks;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Collider2D colliderHit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (colliderHit != null)
            {
                //Debug.Log("Clicked on: " + colliderHit.gameObject.name);
                
                ClickOnItem clickScript = colliderHit.gameObject.GetComponent<ClickOnItem>();
                clickScript.Click();
            }
            else
            {
                missclicks++;
                missclickLabel.text = "Missclicks = " + missclicks;
                //Debug.Log("Nothing clicked.");
            }
        }
    }

    public void AddMissclick()
    {
        missclicks++;
        missclickLabel.text = "Missclicks = " + missclicks;
    }

    public void AddPoint()
    {
        points+=1;
        if(points >= 8)
        {
            SceneTimer sceneTimerInstance = FindObjectOfType<SceneTimer>();
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(sceneTimerInstance && globalManagerInstance)
            {
                globalManagerInstance.AddPoints(sceneTimerInstance.GetTotalSeconds());
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
