using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnItem : MonoBehaviour
{
    public GameObject barObject;
    public Sprite normalSprite; 
    private SpriteRenderer barSpriteRenderer; 

    void Start()
    {
        barSpriteRenderer = barObject.GetComponent<SpriteRenderer>();
        if (barSpriteRenderer != null)
        {
            barSpriteRenderer.sprite = normalSprite;
            barSpriteRenderer.color = Color.black;
        }
    }

    private void OnMouseDown()
    {
        if (barSpriteRenderer != null && normalSprite != null)
        {
            barSpriteRenderer.color = Color.white;
        }
        Destroy(gameObject);
    }
}
