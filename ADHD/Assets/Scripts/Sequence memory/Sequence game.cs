using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sequencegame : MonoBehaviour
{
    public GameObject[] tiles;
    public int score = 0;
    public int error = 0;
    public int sequenceLength = 3;
    public float displayTime = 1.0f;

    private List<int> sequence = new List<int>();
    private int currentIndex = 0;
    private bool playerTurn = false;
    [SerializeField] private TextMeshProUGUI pointsLabel;

    // Start is called before the first frame update
    private void Start()
    {
        // Generate initial sequence
        GenerateSequence();
        // Display the sequence
        StartCoroutine(DisplaySequence());
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTurn)
        {
            // Check for player input
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject clickedTile = hit.collider.gameObject;
                    int tileIndex = System.Array.IndexOf(tiles, clickedTile);
                    if (tileIndex != -1)
                    {
                        // Check if the clicked tile is correct
                        if (tileIndex == sequence[currentIndex])
                        {
                            // Correct tile
                            currentIndex++;
                            tiles[tileIndex].GetComponent<SpriteRenderer>().color = Color.green;
                            StartCoroutine(ResetTileColor(tiles[tileIndex], 0.2f));
                            // Check if the player has completed the sequence
                            if (currentIndex >= sequence.Count)
                            {
                                score++;
                                pointsLabel.text = "Points: " + score;
                                currentIndex = 0;
                                AudioManager.instance.PlaySFX(AudioManager.instance.success);
                                // Generate and display next sequence
                                GenerateSequence();
                                StartCoroutine(DisplaySequence());
                            }
                            else
                            {
                                AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
                            }
                        }
                        else
                        {
                            currentIndex = 0;
                            playerTurn = false;
                            error++;
                            AudioManager.instance.PlaySFX(AudioManager.instance.fail);
                            tiles[tileIndex].GetComponent<SpriteRenderer>().color = Color.red; // Change tile color to green
                            StartCoroutine(ResetOnMiss(tiles[tileIndex], 2));
                        }
                    }
                }
            }
        }
    }

    // Generate a random sequence of tile activations
    private void GenerateSequence()
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, tiles.Length));
        }
    }

    // Display the sequence by activating tiles one by one
    private IEnumerator DisplaySequence()
    {
        playerTurn = false;
        foreach (int index in sequence)
        {
            yield return new WaitForSeconds(displayTime);
            tiles[index].gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
            yield return new WaitForSeconds(displayTime);
            tiles[index].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        playerTurn = true;
    }

    private IEnumerator ResetTileColor(GameObject tile, float delay)
    {
        yield return new WaitForSeconds(delay);
        tile.GetComponent<SpriteRenderer>().color = Color.white; // Reset tile color to white
    }

    private IEnumerator ResetOnMiss(GameObject tile, float delay)
    {
        yield return new WaitForSeconds(delay);
        tile.GetComponent<SpriteRenderer>().color = Color.white; // Reset tile color to white
        GenerateSequence();
        StartCoroutine(DisplaySequence());
    }

    public int GetPoints()
    {
        return score;
    }
}