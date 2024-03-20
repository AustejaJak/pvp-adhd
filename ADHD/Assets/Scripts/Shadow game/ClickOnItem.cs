using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnItem : MonoBehaviour
{
    public bool isHiden = false;
    public int objectId;
    private SpriteRenderer gameObjectSpriteRenderer; 
    public BasePanel basePanelScript;
    public GameEvents gameEventsScript;

    void Start()
    {
        gameObjectSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Click()
    {
        if(isHiden)
        {
            gameObjectSpriteRenderer.color = Color.black;
            basePanelScript.SetWhite(objectId);
            gameEventsScript.AddPoint();
            isHiden = false;
        }
        else{
            gameEventsScript.AddMissclick();
        }
    }
}
