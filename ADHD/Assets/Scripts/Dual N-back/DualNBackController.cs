using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DualNBackController : MonoBehaviour
{
    public Button[] squareButtons;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject feedbackPanel;
    public Text feedbackText;
    public Button positionMatchButton;
    public Button audioMatchButton;

    private List<int> positionSequence = new List<int>(); // Stores the indices of positions
    private List<int> audioSequence = new List<int>(); // Stores the indices of audio clips to play
    private int currentSequenceIndex = 0;
    private int totalRounds = 20; // Total rounds per game
    private int nLevel = 1; // The "N" in N-back

    void Start()
    {
        StartCoroutine(InitialDisplay());
    }

    IEnumerator InitialDisplay()
    {
        feedbackPanel.SetActive(true);
        feedbackText.text = "Dual " + nLevel.ToString() + "-Back";
        feedbackText.color = Color.white;
        yield return new WaitForSeconds(3);
        feedbackPanel.SetActive(false);

        StartCoroutine(GameLoop()); // Start the main game loop
    }

    IEnumerator GameLoop()
    {
        while (currentSequenceIndex < totalRounds)
        {
            GenerateRound();
            yield return new WaitForSeconds(3);
            currentSequenceIndex++;
        }

        CleanupAfterGame();
    }

    void GenerateRound()
    {
        positionMatchButton.interactable = true;
        audioMatchButton.interactable = true;

        feedbackPanel.SetActive(false);

        int newPositionIndex = Random.Range(0, squareButtons.Length);
        int newAudioIndex = Random.Range(0, 2);

        positionSequence.Add(newPositionIndex);
        audioSequence.Add(newAudioIndex);

        HighlightSquare(newPositionIndex);

        audioSource.clip = audioClips[newAudioIndex];
        audioSource.Play();
    }

    void HighlightSquare(int index)
    {
        foreach (Button square in squareButtons)
        {
            square.image.color = Color.blue;
        }
        squareButtons[index].image.color = Color.green;
    }

    public void PositionMatchClicked()
    {
        positionMatchButton.interactable = false;
        feedbackPanel.SetActive(true);

        if (positionSequence.Count > nLevel)
        {
            int currentIndex = positionSequence.Count - 1;
            int nBackIndex = currentIndex - nLevel;

            if (positionSequence[currentIndex] == positionSequence[nBackIndex])
            {
                feedbackText.text = "Correct Position Match!";
                feedbackText.color = Color.green;
            }
            else
            {
                feedbackText.text = "Incorrect Position Match.";
                feedbackText.color = Color.red;
            }
        }
        else
        {
            feedbackText.text = "Not enough data for comparison.";
            feedbackText.color = Color.white;
        }
    }

    public void AudioMatchClicked()
    {
        audioMatchButton.interactable = false;
        feedbackPanel.SetActive(true);

        if (audioSequence.Count > nLevel)
        {
            int currentIndex = audioSequence.Count - 1;
            int nBackIndex = currentIndex - nLevel;

            if (audioSequence[currentIndex] == audioSequence[nBackIndex])
            {
                feedbackText.text = "Correct Audio Match!";
                feedbackText.color = Color.green;
            }
            else
            {
                feedbackText.text = "Incorrect Audio Match.";
                feedbackText.color = Color.red;
            }
        }
        else
        {
            feedbackText.text = "Not enough data for comparison.";
            feedbackText.color = Color.white;
        }
    }

    void CleanupAfterGame()
    {
        positionSequence.Clear();
        audioSequence.Clear();

        feedbackPanel.SetActive(true);
        feedbackText.text = "Game Over";
        feedbackText.color = Color.white;

        Debug.Log("Game over. Sequences cleared.");
    }
}