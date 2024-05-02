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

    private void Start()
    {
        gameObjectSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Click()
    {
        if (isHiden)
        {
            gameObjectSpriteRenderer.color = Color.black;
            basePanelScript.SetWhite(objectId);
            gameEventsScript.AddPoint();
            isHiden = false;
            AudioManager.instance.PlaySFX(AudioManager.instance.success);
        }
        else
        {
            gameEventsScript.AddMissclick();
            AudioManager.instance.PlaySFX(AudioManager.instance.fail);
        }
    }
}