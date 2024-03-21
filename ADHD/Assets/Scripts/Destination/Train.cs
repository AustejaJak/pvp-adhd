using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Transform waypoint; // Waypoint representing the path
    public float speed = 5f; // Speed at which the train moves
    [SerializeField] private TextMesh ScoreLabel;
    [SerializeField] private TextMesh ErrorLabel;
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
                        // Increase score
                        int currentScore = int.Parse(ScoreLabel.text.Split(':')[1].Trim());
                        currentScore++;
                        // Update errorLabel text with the new score
                        ScoreLabel.text = "Score: " + currentScore;
                    }
                    else
                    {
                        int currentScore = int.Parse(ErrorLabel.text.Split(':')[1].Trim());
                        currentScore++;
                        ErrorLabel.text = "Errors: " + currentScore;
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
