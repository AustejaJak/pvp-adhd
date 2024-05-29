using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneControllerFeeding : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab of the ball
    public GameObject fishPrefab; // Prefab of the fish
    public int[] numberOfFishToSpawn; // Array of number of fish to spawn for each level
    public float spawnInterval = 5f; // Interval between spawning balls and fish
    private List<GameObject> balls = new List<GameObject>(); // List to store references to active balls
    private List<GameObject> fish = new List<GameObject>(); // List to store references to active fish
    private float spawnTimer = 0; // Timer to track when to spawn the next ball or fish
    public Vector3 spawnPosition = new Vector3(0, -4, 0); // Spawn position for the balls and fish
    public Vector2 minBounds = new Vector2(-2f, -4f); // Minimum bounds for fish spawning
    public Vector2 maxBounds = new Vector2(2f, 2f); // Maximum bounds for fish spawning
    public bool ballLaunched = false; // Flag to track whether a ball has been launched
    private float gameTime = 0;
    [SerializeField] public TextMesh TimeLabel;
    [SerializeField] public TextMesh ScoreLabel;
    [SerializeField] public TextMesh ErrorLabel;
    [SerializeField] public TextMesh LevelLabel;
    private int currentLevel = 0; // Index to track the current level

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        int currentScore = int.Parse(ScoreLabel.text.Split(':')[1].Trim()) - 1; // Check if the current score equals the level requirement to progress
        spawnTimer += Time.deltaTime;
        gameTime += Time.deltaTime;
        if (TimeLabel != null) // Update timer text
        {
            TimeLabel.text = "Time: " + Mathf.Floor(gameTime).ToString();
        }


        if (gameTime >= 60f)//Out of time
        {
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if (globalManagerInstance)
            {
                int errors = int.Parse(ErrorLabel.text.Split(':')[1].Trim());
                globalManagerInstance.AddScore((int)((currentLevel-(0.25*errors))*(10.0/8.0)));
                globalManagerInstance.AddPoints(currentLevel);
                globalManagerInstance.AddError(errors);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        if (currentLevel < numberOfFishToSpawn.Length && currentScore >= numberOfFishToSpawn[currentLevel])
        {
            currentLevel++;
            if (currentLevel >= numberOfFishToSpawn.Length)//All levels played onto next game
            {
                GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
                if (globalManagerInstance)
                {
                    int errors = int.Parse(ErrorLabel.text.Split(':')[1].Trim());
                    globalManagerInstance.AddScore((int)((currentLevel-(0.25*errors))*(10.0/8.0)));
                    globalManagerInstance.AddPoints(currentLevel);
                    globalManagerInstance.AddError(errors);
                    globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
            ProgressToNextLevel();
        }

        if (spawnTimer >= spawnInterval)
        {
            SpawnBall();
            spawnTimer = 0f;
        }
    }

    void SpawnBall()
    {
        // Check if a ball has been launched
        if (!ballLaunched)
        {
            // Instantiate a new ball at the spawn position
            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            //newBall.SetActive(true);
            BallController Script = newBall.GetComponent<BallController>();
            Script.enabled = true;
            balls.Add(newBall);
            ballLaunched = true;
        }
    }

    void SpawnFish()
    {
        // Ensure there's at least 1 fish spawned
        int numberOfFish = Mathf.Max(1, numberOfFishToSpawn[currentLevel]);

        // Calculate the number of attempts based on the number of fish
        int maxAttempts = numberOfFish * 10;
        int currentAttempts = 0;

        // List to store positions of already spawned fish
        List<Vector3> spawnedPositions = new List<Vector3>();

        // Spawn each fish
        for (int i = 0; i < numberOfFish; i++)
        {
            // Generate random position within the allowed range
            Vector3 randomPosition = Vector3.zero;
            bool validPosition = false;

            // Try finding a valid position within the allowed attempts
            while (!validPosition && currentAttempts < maxAttempts)
            {
                randomPosition = new Vector3(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y), 0f);

                // Check if the random position is far enough from already spawned fish
                validPosition = true;
                foreach (Vector3 spawnedPosition in spawnedPositions)
                {
                    if (Vector3.Distance(randomPosition, spawnedPosition) < 1f)
                    {
                        validPosition = false;
                        break;
                    }
                }

                currentAttempts++;
            }

            // If a valid position was found, spawn the fish
            if (validPosition)
            {
                GameObject newFish = Instantiate(fishPrefab, randomPosition, Quaternion.identity);
                fish.Add(newFish);
                spawnedPositions.Add(randomPosition);
            }
        }
    }

    void ProgressToNextLevel()
    {
        ScoreLabel.text = "Score: 0";

        // Reset the fish and spawn the next level's fish
        ResetFish();
        SpawnFish();

        // Update the level label
        if (LevelLabel != null)
        {
            LevelLabel.text = "Level: " + (currentLevel + 1);
        }
    }

    void ResetFish()
    {
        // Destroy all existing fish
        foreach (GameObject fishObject in fish)
        {
            Destroy(fishObject);
        }
        Fish Script = fishPrefab.GetComponent<Fish>();
        Script.isFed = false;
        Script.transform.position = new Vector3(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y), 0f);

        // Clear the fish list
        fish.Clear();
    }
}
