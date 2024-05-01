using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Color correctColor;

    private MemoryMatrix gameManager;
    private bool isActive = false;

    public void SetGameManager(MemoryMatrix gm)
    {
        gameManager = gm;
    }

    public void OnPointerClick()
    {
        if (isActive)
        {
            gameManager.PlayerSuccess();
            gameObject.GetComponent<SpriteRenderer>().color = correctColor;
        }
        else
        {
            gameManager.PlayerFailure();
        }
    }

    public void SetInvisible()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetActive(bool active)
    {
        isActive = active;
        gameObject.GetComponent<SpriteRenderer>().color = active ? correctColor : Color.white;
    }
}
