using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public int[] switchRotations; // Array of switch rotations
    public Transform[] switchWaypoints;

    [SerializeField] private GameObject switchTransform;

    private int currentPositionIndex = 0; // Index of the current switch rotation

    private void OnMouseDown()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, switchRotations[currentPositionIndex]);

        // Move to the next index
        currentPositionIndex++;
        if (currentPositionIndex >= switchRotations.Length)
        {
            currentPositionIndex = 0; // Reset to the first angle if reached the last one
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
    }

    public Transform GetWaypoint()
    {
        return switchWaypoints[currentPositionIndex];
    }
}