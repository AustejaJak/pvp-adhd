using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public class SceneControllerDestination : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public GameObject[] destinationPrefabs;
    public GameObject switchPrefab;
    public GameObject waypointPrefab;
    public GameObject trackPrefab;

    void Start()
    {
        int randomLevelIndex = UnityEngine.Random.Range(0, 5);
        InitializeLevels(randomLevelIndex);
    }

    void InitializeLevels(int index)
    {

        switch (index)
        {
            case 0:
                //y=-4
                TrainSpawner spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(2f, -4f);//Obeject position

                GameObject Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(0f, -4f);//Obeject position
                Switch switchComponent1 = Switch1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Switch1.transform;//Train comes from

                GameObject Waypoint1 = Instantiate(waypointPrefab);
                Waypoint1.transform.position = new Vector2(-2f, -4f);//Obeject position
                Switch waypointComponent1 = Waypoint1.GetComponent<Switch>();
                switchComponent1.switchWaypoints[1] = Waypoint1.transform;//Train comes from
                switchComponent1.switchRotations[1] = 0;
                //y=-2
                destinationPrefabs[0].transform.position = new Vector2(-2f, -2f);//Obeject position
                waypointComponent1.switchWaypoints[0] = destinationPrefabs[0].transform; //Train comes from
                //y=0
                GameObject Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(0f, 0f);//Obeject position
                Switch switchComponent2 = Switch2.GetComponent<Switch>();
                Array.Resize(ref switchComponent2.switchWaypoints, 3);
                Array.Resize(ref switchComponent2.switchRotations, 3);
                switchComponent1.switchWaypoints[0] = Switch2.transform;//Train comes from
                switchComponent1.switchRotations[0] = 90;

                destinationPrefabs[1].transform.position = new Vector2(-2f, 0f);//Obeject position
                switchComponent2.switchWaypoints[2] = destinationPrefabs[1].transform; //Train comes from
                switchComponent2.switchRotations[2] = 0;

                GameObject Waypoint2 = Instantiate(waypointPrefab);
                Waypoint2.transform.position = new Vector2(2f, 0f);//Obeject position
                Switch waypointComponent2 = Waypoint2.GetComponent<Switch>();
                switchComponent2.switchWaypoints[1] = Waypoint2.transform; //Train comes from
                switchComponent2.switchRotations[1] = 90;
                //y=2
                destinationPrefabs[2].transform.position = new Vector2(0f, 2f);//Obeject position
                switchComponent2.switchWaypoints[0] = destinationPrefabs[2].transform; //Train comes from
                switchComponent2.switchRotations[0] = 270;

                destinationPrefabs[3].transform.position = new Vector2(2f, 02f);//Obeject position
                waypointComponent2.switchWaypoints[0] = destinationPrefabs[3].transform; //Train comes from
                //Track
                GameObject Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -4f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                GameObject Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(0f, -4f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                GameObject Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(0f, 0f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                GameObject Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, 0f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                GameObject Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(2f, 0f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                GameObject Track6 = Instantiate(trackPrefab);
                Track6.transform.position = new Vector3(-2f, -4f, 1f);
                Track6.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track6.transform.localScale = new Vector3(0.1f, 1f, 1f);
                break;
            case 1:
                //from spawner to next connected
                spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(2f, 2f);

                Waypoint1 = Instantiate(waypointPrefab);
                Waypoint1.transform.position = new Vector2(2f, 0f);
                waypointComponent1 = Waypoint1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Waypoint1.transform;

                Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(0f, 0f);
                switchComponent1 = Switch1.GetComponent<Switch>();
                Array.Resize(ref switchComponent1.switchWaypoints, 3);
                Array.Resize(ref switchComponent1.switchRotations, 3);
                waypointComponent1.switchWaypoints[0] = Switch1.transform;

                destinationPrefabs[0].transform.position = new Vector2(0f, 2f);
                switchComponent1.switchWaypoints[0] = destinationPrefabs[0].transform;
                switchComponent1.switchRotations[0] = 90;

                destinationPrefabs[1].transform.position = new Vector2(-2f, 0f);
                switchComponent1.switchWaypoints[1] = destinationPrefabs[1].transform;
                switchComponent1.switchRotations[1] = 180;

                Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(0f, -4f);
                Switch2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                switchComponent2 = Switch2.GetComponent<Switch>();
                switchComponent1.switchWaypoints[2] = Switch2.transform;
                switchComponent1.switchRotations[2] = 0;

                destinationPrefabs[2].transform.position = new Vector2(-2f, -4f);
                switchComponent2.switchWaypoints[0] = destinationPrefabs[2].transform;
                switchComponent2.switchRotations[0] = 270;

                destinationPrefabs[3].transform.position = new Vector2(2f, -4f);
                switchComponent2.switchWaypoints[1] = destinationPrefabs[3].transform;
                switchComponent2.switchRotations[1] = 90;

                //Track
                Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -4f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(0f, -4f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(0f, 0f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, 0f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(2f, 0f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case 2:
                //from spawner to next connected
                spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(-2f, 2f);

                Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(0f, 2f);
                Switch1.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                switchComponent1 = Switch1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Switch1.transform;

                destinationPrefabs[0].transform.position = new Vector2(2f, 2f);
                switchComponent1.switchWaypoints[0] = destinationPrefabs[0].transform;
                switchComponent1.switchRotations[0] = 180;

                Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(0f, -2f);
                Switch2.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                switchComponent2 = Switch2.GetComponent<Switch>();
                switchComponent1.switchWaypoints[1] = Switch2.transform;
                switchComponent1.switchRotations[1] = 270;

                destinationPrefabs[1].transform.position = new Vector2(2f, -2f);
                switchComponent2.switchWaypoints[0] = destinationPrefabs[1].transform;
                switchComponent2.switchRotations[0] = 90;

                GameObject Switch3 = Instantiate(switchPrefab);
                Switch3.transform.position = new Vector2(-2f, -2f);
                Switch switchComponent3 = Switch3.GetComponent<Switch>();
                switchComponent2.switchWaypoints[1] = Switch3.transform;
                switchComponent2.switchRotations[1] = 270;

                destinationPrefabs[2].transform.position = new Vector2(-2f, 0f);
                switchComponent3.switchWaypoints[0] = destinationPrefabs[2].transform;
                switchComponent3.switchRotations[0] = 180;

                destinationPrefabs[3].transform.position = new Vector2(-2f, -4f);
                switchComponent3.switchWaypoints[1] = destinationPrefabs[3].transform;
                switchComponent3.switchRotations[1] = 0;

                Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -2f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(0f, -2f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(0f, 2f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, 2f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(-2f, -4f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 0);
                break;
            case 3:
                //from spawner to next connected
                spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(2f, 2f);

                Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(2f, -2f);
                Switch1.transform.rotation = Quaternion.Euler(0f, 0f, 180);
                switchComponent1 = Switch1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Switch1.transform;

                destinationPrefabs[0].transform.position = new Vector2(2f, -4f);
                switchComponent1.switchWaypoints[0] = destinationPrefabs[0].transform;
                switchComponent1.switchRotations[0] = 90;

                Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(-2f, -2f);
                switchComponent2 = Switch2.GetComponent<Switch>();
                switchComponent1.switchWaypoints[1] = Switch2.transform;
                switchComponent1.switchRotations[1] = 180;

                destinationPrefabs[1].transform.position = new Vector2(-2f, -4f);
                switchComponent2.switchWaypoints[1] = destinationPrefabs[1].transform;
                switchComponent2.switchRotations[1] = 0;

                Switch3 = Instantiate(switchPrefab);
                Switch3.transform.position = new Vector2(-2f, 0f);
                switchComponent3 = Switch3.GetComponent<Switch>();
                switchComponent2.switchWaypoints[0] = Switch3.transform;
                switchComponent2.switchRotations[0] = 180;

                destinationPrefabs[2].transform.position = new Vector2(-2f, 2f);
                switchComponent3.switchWaypoints[0] = destinationPrefabs[2].transform;
                switchComponent3.switchRotations[0] = 270;

                destinationPrefabs[3].transform.position = new Vector2(0f, 0f);
                switchComponent3.switchWaypoints[1] = destinationPrefabs[3].transform;
                switchComponent3.switchRotations[1] = 0;

                Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -2f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(2f, -2f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(0f, 0f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, -4f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(-2f, -4f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 0);
                Track6 = Instantiate(trackPrefab);
                Track6.transform.position = new Vector3(-2f, 0f, 1f);
                Track6.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track6.transform.localScale = new Vector3(0.1f, 1f, 1f);
                break;
            case 4:
                //from spawner to next connected
                spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(-2f, -4f);

                Waypoint1 = Instantiate(waypointPrefab);
                Waypoint1.transform.position = new Vector2(2f, -4f);
                waypointComponent1 = Waypoint1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Waypoint1.transform;

                Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(2f, -2f);
                Switch1.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                switchComponent1 = Switch1.GetComponent<Switch>();
                waypointComponent1.switchWaypoints[0] = Switch1.transform;

                destinationPrefabs[0].transform.position = new Vector2(0f, -2f);
                switchComponent1.switchWaypoints[0] = destinationPrefabs[0].transform;
                switchComponent1.switchRotations[0] = 0;

                Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(2f, 0f);
                switchComponent2 = Switch2.GetComponent<Switch>();
                switchComponent1.switchWaypoints[1] = Switch2.transform;
                switchComponent1.switchRotations[1] = 90;

                destinationPrefabs[1].transform.position = new Vector2(2f, 2f);
                switchComponent2.switchWaypoints[0] = destinationPrefabs[1].transform;
                switchComponent2.switchRotations[0] = 90;

                Switch3 = Instantiate(switchPrefab);
                Switch3.transform.position = new Vector2(-2f, 0f);
                switchComponent3 = Switch3.GetComponent<Switch>();
                switchComponent2.switchWaypoints[1] = Switch3.transform;
                switchComponent2.switchRotations[1] = 0;

                destinationPrefabs[2].transform.position = new Vector2(-2f, 2f);
                switchComponent3.switchWaypoints[0] = destinationPrefabs[2].transform;
                switchComponent3.switchRotations[0] = 180;

                destinationPrefabs[3].transform.position = new Vector2(-2f, -2f);
                switchComponent3.switchWaypoints[1] = destinationPrefabs[3].transform;
                switchComponent3.switchRotations[1] = 0;

                Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -4f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(2f, -4f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(2f, -2f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, 0f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(2f, 0f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track6 = Instantiate(trackPrefab);
                Track6.transform.position = new Vector3(-2f, 0f, 1f);
                Track6.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track6.transform.localScale = new Vector3(0.1f, 1f, 1f);
                GameObject Track7 = Instantiate(trackPrefab);
                Track7.transform.position = new Vector3(-2f, -2f, 1f);
                Track7.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track7.transform.localScale = new Vector3(0.1f, 1f, 1f);
                break;
            default:
                spawnerComponent = spawnerPrefab.GetComponent<TrainSpawner>();
                spawnerPrefab.transform.position = new Vector2(-2f, -4f);

                Waypoint1 = Instantiate(waypointPrefab);
                Waypoint1.transform.position = new Vector2(2f, -4f);
                waypointComponent1 = Waypoint1.GetComponent<Switch>();
                spawnerComponent.initialWaypoint = Waypoint1.transform;

                Switch1 = Instantiate(switchPrefab);
                Switch1.transform.position = new Vector2(2f, -2f);
                Switch1.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                switchComponent1 = Switch1.GetComponent<Switch>();
                waypointComponent1.switchWaypoints[0] = Switch1.transform;

                destinationPrefabs[0].transform.position = new Vector2(0f, -2f);
                switchComponent1.switchWaypoints[0] = destinationPrefabs[0].transform;
                switchComponent1.switchRotations[0] = 0;

                Switch2 = Instantiate(switchPrefab);
                Switch2.transform.position = new Vector2(2f, 0f);
                switchComponent2 = Switch2.GetComponent<Switch>();
                switchComponent1.switchWaypoints[1] = Switch2.transform;
                switchComponent1.switchRotations[1] = 90;

                destinationPrefabs[1].transform.position = new Vector2(2f, 2f);
                switchComponent2.switchWaypoints[0] = destinationPrefabs[1].transform;
                switchComponent2.switchRotations[0] = 90;

                Switch3 = Instantiate(switchPrefab);
                Switch3.transform.position = new Vector2(-2f, 0f);
                switchComponent3 = Switch3.GetComponent<Switch>();
                switchComponent2.switchWaypoints[1] = Switch3.transform;
                switchComponent2.switchRotations[1] = 0;

                destinationPrefabs[2].transform.position = new Vector2(-2f, 2f);
                switchComponent3.switchWaypoints[0] = destinationPrefabs[2].transform;
                switchComponent3.switchRotations[0] = 180;

                destinationPrefabs[3].transform.position = new Vector2(-2f, -2f);
                switchComponent3.switchWaypoints[1] = destinationPrefabs[3].transform;
                switchComponent3.switchRotations[1] = 0;

                Track1 = Instantiate(trackPrefab);
                Track1.transform.position = new Vector3(2f, -4f, 1f);
                Track1.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track2 = Instantiate(trackPrefab);
                Track2.transform.position = new Vector3(2f, -4f, 1f);
                Track2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track3 = Instantiate(trackPrefab);
                Track3.transform.position = new Vector3(2f, -2f, 1f);
                Track3.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track3.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track4 = Instantiate(trackPrefab);
                Track4.transform.position = new Vector3(2f, 0f, 1f);
                Track4.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track4.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track5 = Instantiate(trackPrefab);
                Track5.transform.position = new Vector3(2f, 0f, 1f);
                Track5.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Track6 = Instantiate(trackPrefab);
                Track6.transform.position = new Vector3(-2f, 0f, 1f);
                Track6.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track6.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Track7 = Instantiate(trackPrefab);
                Track7.transform.position = new Vector3(-2f, -2f, 1f);
                Track7.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Track7.transform.localScale = new Vector3(0.1f, 1f, 1f);
                break;
        }
    }
}
