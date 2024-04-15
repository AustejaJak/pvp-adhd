using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Transform waypoint; // Waypoint representing the path
    public float speed = 5f; // Speed at which the train moves
    private int ID;

    void Update()
    {
        if (waypoint != null)
        {
            // Move the train towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);

            // Check if the train has reached the waypoint
            if (Vector3.Distance(transform.position, waypoint.position) < 0.01f)
            {
                //Object has reached the end destination
                if (waypoint.gameObject.name.Contains("Destination"))
                {
                    string destinationName = waypoint.gameObject.name;
                    int destinationNumber = int.Parse(destinationName.Substring(destinationName.IndexOf("Destination") + "Destination".Length));

                    // Check if the destination number matches the train's ID
                    if (destinationNumber == ID)
                    {
                        TrainSpawner trainSpawnerInstance = FindObjectOfType<TrainSpawner>();
                        trainSpawnerInstance.AddScore();
                    }
                    else
                    {
                        TrainSpawner trainSpawnerInstance = FindObjectOfType<TrainSpawner>();
                        trainSpawnerInstance.AddError();
                    }

                    Destroy(gameObject);
                    return;
                }

                //Get next waypoint
                Switch switchObj = waypoint.GetComponent<Switch>();
                Transform switchWaypoints = switchObj.GetWaypoint();
                if (switchWaypoints != null)
                {
                    // Update the train's waypoints to follow the switch waypoints
                    waypoint = switchWaypoints;
                }
            }
        }
    }

    public void SetTrainIDAndSprite(int id, Sprite trainSprite)
    {
        ID = id;
        GetComponent<SpriteRenderer>().sprite = trainSprite;
        GetComponent<Transform>().localScale = new Vector3(4, 4, 1);
    }

    public int GetTrainID()
    {
        return ID;
    }

    public void SetInitialWaypoint(Transform initialWaypoint)
    {
        waypoint = initialWaypoint;
    }
}