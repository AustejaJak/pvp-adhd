using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    [SerializeField] private TextMeshProUGUI errorLabel;
    public int pointsNeeded = 10;
    private int errors = 0;
    private int points = 0;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    public float minSwipeDistance = 50f;

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerUI();

        if (pointsNeeded <= points)
        {
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if (globalManagerInstance)
            {
                globalManagerInstance.AddScore((int)(10-(2*(timer)/60.0)-(errors*0.5)*10));
                globalManagerInstance.AddPoints((int)timer);
                globalManagerInstance.AddError(errors);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        Swipe();
    }

    private void UpdateTimerUI()
    {
        timeLabel.text = "" + Mathf.Round(timer);
    }

    private void Swipe()
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
                                AudioManager.instance.PlaySFX(AudioManager.instance.success);
                            }
                            else
                            {
                                errors++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
                            }
                            swarm.Destroy();
                        }
                        //Left swipe
                        else
                        {
                            if (swarm.swipeDirection == "Left")
                            {
                                points++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.success);
                            }
                            else
                            {
                                errors++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
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
                                AudioManager.instance.PlaySFX(AudioManager.instance.success);
                            }
                            else
                            {
                                errors++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
                            }
                            swarm.Destroy();
                        }
                        //Down swipe
                        else
                        {
                            if (swarm.swipeDirection == "Down")
                            {
                                points++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.success);
                            }
                            else
                            {
                                errors++;
                                AudioManager.instance.PlaySFX(AudioManager.instance.fail);
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