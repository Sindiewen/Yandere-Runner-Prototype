using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchiroDistanceController : MonoBehaviour {

    // Class variables
    //  Public
    public float distanceTraveled = 0;      // Stores the current distance traveled

    //  Private
    private Vector2 lastPosition;           // Stores the last position


    private void Start()
    {
        // Initialized the last position as this current position
        lastPosition = transform.position;
    }


    private void Update()
    {
        // Updates the distance traveled between this current position and the last position
        distanceTraveled += Vector2.Distance(transform.position, lastPosition);

        // Updates the last position with the new position
        lastPosition = transform.position;
    }
}
