using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject sceneController;
    public float moveSpeed = 10f; // Initial speed of the ball
    public float stayDuration = 2f; // Duration for the ball to stay at the target position
    private Vector3 initialPosition; // Initial position of the ball
    private Vector3 targetPosition; // Target position clicked by the user
    private bool isMoving = false; // Flag to track if the ball is currently moving
    private bool hasReachedTarget = false; // Flag to track if the ball has reached the target position
    private float stayTimer = 0f; // Timer to track how long the ball has stayed at the target position
    private BoxCollider2D AreaCollider;
    public bool hasFedFish = false;

    void Start()
    {
        initialPosition = transform.position;
        AreaCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isMoving && !hasReachedTarget && Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
            isMoving = true;
            SceneControllerFeeding Script = sceneController.GetComponent<SceneControllerFeeding>();
            Script.ballLaunched = false;
        }

        if (isMoving)
        {
            MoveBall();
        }
        else if (hasReachedTarget && stayTimer > 0)
        {
            AreaCollider.enabled = true;
            stayTimer -= Time.deltaTime;
            if (stayTimer <= 0)
            {
                ResetBall();
            }
        }
    }

    void SetTargetPosition()
    {
        // Get the position clicked by the user
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // Ensure the z-coordinate is 0 since we're working in 2D
    }

    void MoveBall()
    {
        // Move the ball towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the ball has reached the target position
        if (transform.position == targetPosition)
        {
            // Stop the ball from moving
            isMoving = false;
            hasReachedTarget = true;
            // Start the timer for the stay duration
            stayTimer = stayDuration;
        }
    }

    public void ResetBall()
    {
        // Reset the ball to its initial position and reset flags
        transform.position = initialPosition;
        isMoving = false;
        hasReachedTarget = false;
        Destroy(gameObject);
    }

    public void LaunchFromSpawnPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        targetPosition.z = 0; // Ensure the z-coordinate is 0 since we're working in 2D
        isMoving = true;
    }
}
