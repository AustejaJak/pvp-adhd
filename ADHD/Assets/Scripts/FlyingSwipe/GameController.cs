using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float totalTime = 60f;
    private float timer;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    [SerializeField] private TextMeshProUGUI errorLabel;
    private int errors = 0;
    private int points = 0;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;

    // Minimum distance required for a swipe to be registered
    public float minSwipeDistance = 50f;

    void Start()
    {
        timer = totalTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if(globalManagerInstance)
            {
                globalManagerInstance.AddPoints(points);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        Swipe();
    }

    void UpdateTimerUI()
    {
        timeLabel.text = "" + Mathf.Round(timer);
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            Swarm swarm = FindObjectOfType<Swarm>();

            endTouchPosition = Input.mousePosition;
            isSwiping = false;
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            float swipeDistance = swipeDirection.magnitude;

            if (swarm != null)
            {
                if (swipeDistance >= minSwipeDistance)
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        //Right swipe
                        if (swipeDirection.x > 0)
                        {
                            if (swarm.swipeDirection == "Right")
                            {
                                points++;
                            }
                            else
                            {
                                errors++;
                            }
                            swarm.Destroy();
                        }
                        //Left swipe
                        else
                        {
                            if (swarm.swipeDirection == "Left")
                            {
                                points++;
                            }
                            else
                            {
                                errors++;
                            }
                            swarm.Destroy();
                        }
                    }
                    else
                    {
                        //Up swipe
                        if (swipeDirection.y > 0)
                        {
                            if (swarm.swipeDirection == "Up")
                            {
                                points++;
                            }
                            else
                            {
                                errors++;
                            }
                            swarm.Destroy();
                        }
                        //Down swipe
                        else
                        {
                            if (swarm.swipeDirection == "Down")
                            {
                                points++;
                            }
                            else
                            {
                                errors++;
                            }
                            swarm.Destroy();
                        }
                    }
                    pointsLabel.text = "Points:" + points;
                    errorLabel.text = "Errors:" + errors;
                }   
            }
        }
    }
}
