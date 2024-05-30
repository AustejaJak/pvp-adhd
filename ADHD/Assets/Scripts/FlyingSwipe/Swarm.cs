using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public Transform[] flys;
    public string direction;
    public string swipeDirection;

    void Start()
    {
        Transform randomFly = flys[Random.Range(0, flys.Length)];

        List<string> rotations = new List<string>{"Left", "Right", "Up", "Down"};

        rotations.Remove(direction);

        string randomRotation = rotations[Random.Range(0, rotations.Count)];

        swipeDirection = randomRotation;

        switch (randomRotation)
        {
            case "Left":
                randomFly.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case  "Right":
                randomFly.rotation = Quaternion.Euler(0f, 0f, 270f);
                break;            
            case "Up":
                randomFly.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;            
            case "Down":
                randomFly.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;            
        }
    }

    public void Destroy()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null) spawner.SetReadToSpawn();
        Destroy(gameObject);
    }
    void Update()
    {
        if (gameObject.transform.position.x < -5 || gameObject.transform.position.x > 5 || gameObject.transform.position.y > 9 || gameObject.transform.position.y < -9)
        {
            Destroy();
        }
    }
}
