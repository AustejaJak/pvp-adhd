using UnityEngine;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the fish
    private Vector2 minBounds = new Vector2(-4f, -7f); // Minimum bounds for fish movement
    private Vector2 maxBounds = new Vector2(4f, 8f); // Maximum bounds for fish movement
    private Vector3 targetPosition; // Target position for the fish
    public bool isFed = false; // Flag to track if the fish is fed
    private BoxCollider2D feedingAreaCollider; // Reference to the fish's BoxCollider2D component
    public GameObject sceneController;

    void Start()
    {
        SetRandomTargetPosition();
        feedingAreaCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate the fish towards the target position
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            // Rotate the collider with the fish
            feedingAreaCollider.transform.rotation = transform.rotation;
        }

        // Check if the fish has reached the target position 
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Set a new random target position
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        // Generate a random position within the allowed range
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        targetPosition = new Vector3(randomX, randomY, 0f);
    }

    // Method to handle the closest ball after all collisions are processed
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BallController ball = other.GetComponent<BallController>();
            if(!ball.hasFedFish)
            {
                ball.hasFedFish = true;
                Destroy(other.gameObject);
                SceneControllerFeeding Script = sceneController.GetComponent<SceneControllerFeeding>();
                if (!isFed)
                {
                    isFed = true;
                    int currentScore = int.Parse(Script.ScoreLabel.text.Split(':')[1].Trim()); // Extract the current score
                    Script.ScoreLabel.text = "Score: " + (currentScore + 1);
                }
                else
                {
                    int currentErrors = int.Parse(Script.ErrorLabel.text.Split(':')[1].Trim()); // Extract the current number of errors
                    Script.ErrorLabel.text = "Errors: " + (currentErrors + 1);
                }
            }
        }
    }
}
