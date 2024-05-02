using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 6;
    public const int gridCols = 2;
    public const float offsetX = 1.5f;
    public const float offsetY = 1.05f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh timerLabel;
    [SerializeField] private TextMesh errorLabel;

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    private void Start()
    {
        SetupCards();
    }

    private void Update()
    {
        // Update the timer if it's running
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void SetupCards()
    {
        Vector3 startPos = originalCard.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        numbers = ShuffleArray(numbers); //This is a function we will create in a minute!

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private void StartTimer()
    {
        timerRunning = true;
    }

    private void UpdateTimerDisplay()
    {
        timerLabel.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    private int _error = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
            StartTimer();
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "" + _score;
            AudioManager.instance.PlaySFX(AudioManager.instance.success);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();

            if (_firstRevealed.revealedBefore && _secondRevealed.revealedBefore)
            {
                _error += 2;
            }
            else if (_secondRevealed.revealedBefore)
            {
                _error += 1;
            }
            _firstRevealed.revealedBefore = true;
            _secondRevealed.revealedBefore = true;
            errorLabel.text = "Errors: " + _error;
        }

        _firstRevealed = null;
        _secondRevealed = null;

        if (_score == 6)
        {
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if (globalManagerInstance)
            {
                globalManagerInstance.AddPoints((int)elapsedTime);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("concentration");
    }
}