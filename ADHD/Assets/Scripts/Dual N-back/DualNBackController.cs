using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;

public class DualNBackController : MonoBehaviour
{
    public Button[] squareButtons;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject feedbackPanel;
    public Text feedbackText;
    public Button positionMatchButton;
    public Button audioMatchButton;
    public Sprite bigButton;
    public Sprite bigButtonNull;

    private readonly List<int> _positionHistory = new(); // Stores history of positions
    private readonly List<int> _audioHistory = new(); // Stores history of audio clips to play
    private int _currentRound;
    private const int TotalRounds = 20; // Total rounds per game
    private const int NLevel = 2; // The "N" in N-back
    private const int AudioAmount = 4; // How many letters? 3 = a, b, c
    private int _positionMatches;
    private int _audioMatches;
    private int _score;
    private int _errors;

    private void Start()
    {
        StartCoroutine(InitialDisplay());
    }

    private IEnumerator InitialDisplay()
    {
        feedbackPanel.SetActive(true);
        feedbackText.text = "Dual " + NLevel + "-Back";
        feedbackText.color = Color.white;
        yield return new WaitForSeconds(3);
        feedbackPanel.SetActive(false);

        StartCoroutine(GameLoop()); // Start the main game loop
    }

    private IEnumerator GameLoop()
    {
        while (_currentRound < TotalRounds)
        {
            GenerateRound();
            yield return new WaitForSeconds(3);
            _currentRound++;
        }

        CleanupAfterGame();
    }

    private void GenerateRound()
    {
        // Check if the player missed a match in the previous round
        if (_positionHistory.Count > NLevel && _positionHistory[^1] == _positionHistory[_positionHistory.Count - NLevel - 1] && !positionMatchButton.interactable)
        {
            _errors++;
        }
        if (_audioHistory.Count > NLevel && _audioHistory[^1] == _audioHistory[_audioHistory.Count - NLevel - 1] && !audioMatchButton.interactable)
        {
            _errors++;
        }

        positionMatchButton.interactable = true;
        audioMatchButton.interactable = true;

        feedbackPanel.SetActive(false);

        int newPositionIndex;
        int newAudioIndex;

        // If it's one of the last 5 rounds and there weren't 3 matches, force them
        if (_currentRound >= TotalRounds - 5 && (_positionMatches < 3 || _audioMatches < 3) && _positionHistory.Count >= NLevel && _audioHistory.Count >= NLevel)
        {
            newPositionIndex = _positionHistory[^NLevel];
            newAudioIndex = _audioHistory[^NLevel];
        }
        else
        {
            newPositionIndex = Random.Range(0, squareButtons.Length);
            newAudioIndex = Random.Range(0, AudioAmount);
        }

        // Check if the new round is a match
        if (_positionHistory.Count > NLevel && newPositionIndex == _positionHistory[^NLevel])
        {
            _positionMatches++;
        }
        if (_audioHistory.Count > NLevel && newAudioIndex == _audioHistory[^NLevel])
        {
            _audioMatches++;
        }

        _positionHistory.Add(newPositionIndex);
        _audioHistory.Add(newAudioIndex);

        HighlightSquare(newPositionIndex);

        audioSource.clip = audioClips[newAudioIndex];
        audioSource.Play();
    }

    private void HighlightSquare(int index)
    {
        foreach (Button square in squareButtons)
        {
            square.image.sprite = bigButtonNull;
        }
        squareButtons[index].image.sprite = bigButton;
    }

    public void PositionMatchClicked()
    {
        positionMatchButton.interactable = false;
        feedbackPanel.SetActive(true);

        if (_positionHistory.Count > NLevel)
        {
            int currentIndex = _positionHistory.Count - 1;
            int nBackIndex = currentIndex - NLevel;

            if (_positionHistory[currentIndex] == _positionHistory[nBackIndex])
            {
                _score++;
                feedbackText.text = "Correct Position Match!";
                feedbackText.color = Color.green;
                AudioManager.instance.PlaySFX(AudioManager.instance.success);
            }
            else
            {
                _errors++;
                feedbackText.text = "Incorrect Position Match.";
                feedbackText.color = Color.red;
                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
            }
        }
        else
        {
            feedbackText.text = "Not enough data for comparison.";
            feedbackText.color = Color.white;
            AudioManager.instance.PlaySFX(AudioManager.instance.fail);
        }
    }

    public void AudioMatchClicked()
    {
        audioMatchButton.interactable = false;
        feedbackPanel.SetActive(true);

        if (_audioHistory.Count > NLevel)
        {
            int currentIndex = _audioHistory.Count - 1;
            int nBackIndex = currentIndex - NLevel;

            if (_audioHistory[currentIndex] == _audioHistory[nBackIndex])
            {
                _score++;
                feedbackText.text = "Correct Audio Match!";
                feedbackText.color = Color.green;
                AudioManager.instance.PlaySFX(AudioManager.instance.success);
            }
            else
            {
                _errors++;
                feedbackText.text = "Incorrect Audio Match.";
                feedbackText.color = Color.red;
                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
            }
        }
        else
        {
            feedbackText.text = "Not enough data for comparison.";
            feedbackText.color = Color.white;
            AudioManager.instance.PlaySFX(AudioManager.instance.fail);
        }
    }

    public int GetScore()
    {
        return _score;
    }

    private void CleanupAfterGame()
    {
        _positionHistory.Clear();
        _audioHistory.Clear();

        feedbackPanel.SetActive(true);
        feedbackText.text = "Game Over";
        feedbackText.color = Color.white;

        Debug.Log("Game over. Sequences cleared.");
    }
}