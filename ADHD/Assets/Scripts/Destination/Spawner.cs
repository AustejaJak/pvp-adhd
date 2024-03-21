using System.Collections;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainPrefab; // Prefab of the train object to spawn
    public float spawnInterval = 5f; // Interval between train spawns
    public int maxTrains = 5; // Maximum number of trains to spawn
    public Transform initialWaypoint; // Initial waypoint for the spawned trains
    public Sprite[] trainSprites; // Array of sprites for the trains
    private int currentTrains = 0; // Current number of spawned trains
    [SerializeField] private TextMesh TimeLabel;
    private float timer = 0f;

    void Start()
    {
        // Start spawning trains
        StartCoroutine(SpawnTrainRoutine());
    }

    void Update()
    {
        // Update timer
        timer += Time.deltaTime;
        TimeLabel.text = "Time: " + Mathf.Round(timer);
    }


    IEnumerator SpawnTrainRoutine()
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

            //Set id and sprite
            int trainID = Random.Range(1, trainSprites.Length+1);//Determined by Total amount of destinations
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
