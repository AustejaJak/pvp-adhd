using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryMatrix : MonoBehaviour
{
    public int initialTileNumber = 2;
    public int maxLevels;
    public float tileSpacing = 1.2f;
    public GameObject tilePrefab;
    public Transform gridParent;
    public int maxColumns = 5;
    public float yScaling = 0;

    private int score = 0;
    private int level = 1;
    private int tilesGuessed;
    private int neededTiles;
    private bool haveStarted = false;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    private List<Tile> activeTiles = new List<Tile>();

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GenerateGrid(level);
    }

    private void GenerateGrid(int level)
    {
        int active = 0;
        int tileCount = 0;

        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        activeTiles.Clear();

        int numTiles = initialTileNumber * level;
        tilesGuessed = 0;
        neededTiles = (int)(numTiles * 0.5);

        int numRows = Mathf.CeilToInt((float)numTiles / maxColumns);
        int numColumns = Mathf.Min(numTiles, maxColumns);

        Vector3 startPos = new Vector3(-(numColumns - 1) * tileSpacing / 2f, (numRows - 1) * tileSpacing / 2f + yScaling, -1);

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                tileCount++;
                Vector3 tilePos = startPos + new Vector3(col * tileSpacing, -row * tileSpacing, -1);
                GameObject tileGO = Instantiate(tilePrefab, tilePos, Quaternion.identity, gridParent);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.SetGameManager(this);
                activeTiles.Add(tile);

                bool setActive = Random.Range(0f, 1f) < 0.5f;

                if (numTiles * 0.5 - active == numTiles - tileCount + 1) setActive = true;

                if (active == numTiles * 0.5) setActive = false;

                tile.SetActive(setActive);
                if (setActive) active++;
            }
        }

        haveStarted = false;
    }

    public void PlayerSuccess()
    {
        tilesGuessed++;
        if (tilesGuessed == neededTiles)
        {
            score += level * 10;
            pointsLabel.text = "Points: " + score;
            if (level < maxLevels) level++;
            GenerateGrid(level);
            AudioManager.instance.PlaySFX(AudioManager.instance.success);
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        }
    }

    public void PlayerFailure()
    {
        if (level > 1)
        {
            level--;
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.fail);
        GenerateGrid(level);
    }

    private void allTilesInvisible()
    {
        foreach (Tile tile in activeTiles)
        {
            tile.SetInvisible();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!haveStarted)
            {
                allTilesInvisible();
                haveStarted = true;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject clickedTile = hit.collider.gameObject;
                Tile tileManager = clickedTile.GetComponent<Tile>();
                if (tileManager)
                {
                    tileManager.OnPointerClick();
                }
            }
        }
    }

    public int GetPoints()
    {
        return score;
    }
}