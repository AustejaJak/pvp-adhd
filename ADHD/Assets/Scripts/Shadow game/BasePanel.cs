using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    public RectTransform panelTransform;
    public int sortingOrderOffset = 1;
    private Canvas canvas;
    private int panelSortingOrder;
    private List<GameObject> slots = new List<GameObject>();

    public void SpawnGrid(List<GameObject> hiddenItems)
    {
        canvas = panelTransform.GetComponentInParent<Canvas>();
        panelSortingOrder = canvas.sortingOrder;
        int rows = 2;
        int cols = 4;

        float cellWidth = panelTransform.rect.width / cols;
        float cellHeight = panelTransform.rect.height / rows;

        int sortingOrder = panelSortingOrder + sortingOrderOffset;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                SpriteRenderer spriteRenderer = hiddenItems[i*4+j].GetComponent<SpriteRenderer>();
                Transform objTransform  = hiddenItems[i*4+j].GetComponent<Transform>();
                Quaternion rotation = objTransform.rotation;
                Sprite sprite = spriteRenderer.sprite;

                Vector3 localPosition = new Vector3(
                    (j * cellWidth) + (cellWidth / 2) - (panelTransform.rect.width/2),
                    -(i * cellHeight) - (cellHeight / 2) + (panelTransform.rect.height/2),
                    0f);

                // Instantiate an empty GameObject
                GameObject obj = new GameObject("Item");

                RectTransform rectTransform = obj.AddComponent<RectTransform>();
                rectTransform.SetParent(panelTransform, false);
                rectTransform.localPosition = localPosition;
                rectTransform.localScale *= 2f;
                rectTransform.eulerAngles = rotation.eulerAngles;

                Image imageComponent = obj.AddComponent<Image>();
                imageComponent.sprite = sprite;
                imageComponent.canvas.sortingOrder = sortingOrder;
                imageComponent.color = Color.black;
                imageComponent.SetNativeSize();

                slots.Add(obj);
            }
        }
    }

    public void SetWhite(int id)
    {
        Image image = slots[id].GetComponent<Image>();

        image.color = Color.white;
    }
}
