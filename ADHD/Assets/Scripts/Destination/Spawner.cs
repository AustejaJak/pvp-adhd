using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainPrefab; // Prefab of the train object to spawn
    public float spawnInterval = 5f; // Interval between train spawns
    public int maxTrains = 5; // Maximum number of trains to spawn
    public Transform initialWaypoint; // Initial waypoint for the spawned trains
    public Sprite[] trainSprites; // Array of sprites for the trains
    private int currentTrains = 0; // Current number of spawned trains
    [SerializeField] private TextMesh TimeLabel;
    [SerializeField] private TextMesh ScoreLabel;
    [SerializeField] private TextMesh ErrorLabel;
    [SerializeField] private float timer;
    private int score = 0;
    private int error = 0;

    private void Start()
    {
        // Start spawning trains
        StartCoroutine(SpawnTrainRoutine());
    }

    private void Update()
    {
        // Update timer
        timer -= Time.deltaTime;
        TimeLabel.text = "Time: " + Mathf.Round(timer);
        if (timer <= 0)
        {
            GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
            if (globalManagerInstance)
            {
                globalManagerInstance.AddScore((int)((score-(0.25*error))*(10.0/25.0)));
                globalManagerInstance.AddPoints(score);
                globalManagerInstance.AddError(error);
                globalManagerInstance.AddScene(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void AddScore()
    {
        score++;
        ScoreLabel.text = "Score: " + score;
        AudioManager.instance.PlaySFX(AudioManager.instance.success);
    }

    public void AddError()
    {
        error++;
        ErrorLabel.text = "Errors: " + error;
        AudioManager.instance.PlaySFX(AudioManager.instance.fail);
    }

    private IEnumerator SpawnTrainRoutine()
    {
        while (currentTrains < maxTrains)
        {
            //Create train on Spawner
            GameObject newTrain = Instantiate(trainPrefab, transform.position, Quaternion.identity);
            Train trainScript = newTrain.GetComponent<Train>();
            if (trainScript != null)
            {
                trainScript.SetInitialWaypoint(initialWaypoint);//Give its initial waypoint
            }
            newTrain.SetActive(true);

            //Set id and sprite
            int trainID = Random.Range(1, trainSprites.Length + 1);//Determined by Total amount of destinations
            Sprite trainSprite = null;
            if (trainSprites != null && trainSprites.Length > 0)
            {
                // Ensure the selected ID is within the bounds of the trainSprites array
                int spriteIndex = Mathf.Clamp(trainID - 1, 0, trainSprites.Length - 1);
                trainSprite = trainSprites[spriteIndex];
            }
            trainScript.SetTrainIDAndSprite(trainID, trainSprite);

            currentTrains++;

            // Wait for the specified interval before spawning the next train
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}