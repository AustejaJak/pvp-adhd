using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public float spawnInterval = 2f;
    public float moveSpeed = 5f;
    public Transform[] spawners;
    private Quaternion rotation;
    private float speed;
    
    void Start()
    {
        StartCoroutine(SpawnPrefabCoroutine());
    }

    void Update()
    {
         
    }

    IEnumerator SpawnPrefabCoroutine()
    {
        while (true)
        {
            GameObject randomSwarm = prefabToSpawn[Random.Range(0, prefabToSpawn.Length)];
            Transform randomSpawner = spawners[Random.Range(0, spawners.Length)];
            GameObject spawnedPrefab = Instantiate(randomSwarm, randomSpawner.position, Quaternion.identity);
            speed = moveSpeed;
            Vector3 moveDirection = Vector3.zero;
            Swarm swarm = spawnedPrefab.GetComponent<Swarm>();

            if (randomSpawner.position.x < -1)
            {
                moveDirection = Vector3.right;
                rotation = Quaternion.Euler(0f, 0f, 270f);
                speed /= 2;
                swarm.direction = "Right";
            }

            else if (randomSpawner.position.x > 1)
            {
                moveDirection = Vector3.left;
                rotation = Quaternion.Euler(0f, 0f, 90f);
                speed /= 2;
                swarm.direction = "Left";
            }
            else if (randomSpawner.position.y < -1)
            {
                moveDirection = Vector3.up;
                rotation = Quaternion.Euler(0f, 0f, 0f);
                swarm.direction = "Up";
            }
            else
            {
                moveDirection = Vector3.down;
                rotation = Quaternion.Euler(0f, 0f, 180f);
                swarm.direction = "Down";
            }

            Rigidbody2D rb = spawnedPrefab.GetComponent<Rigidbody2D>();
            spawnedPrefab.transform.rotation = rotation;

            if (rb != null)
            {
                rb.velocity = moveDirection.normalized * speed;
            }
            else
            {
                Debug.LogWarning("The prefab does not have a Rigidbody component.");
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
