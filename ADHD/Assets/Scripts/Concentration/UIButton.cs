using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private TextMesh textMeshComponent;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.gray;

    public void OnMouseOver()
    {
        textMeshComponent.color = highlightColor;
    }

    public void OnMouseExit()
    {
        textMeshComponent.color = Color.white;
    }

    public void OnMouseDown()
    {

    }

    public void OnMouseUp()
    {
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }


}