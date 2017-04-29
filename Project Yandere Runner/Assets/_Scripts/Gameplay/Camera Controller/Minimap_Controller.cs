using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Controller : MonoBehaviour
{
	// public variables
	//	Players
	[HeaderAttribute("Players")]
	public Transform Ichiro;		    // Reference to the Ichiro Player
	public Transform Yumi;			    // Reference to the Yumi Player
	


    
	// Private Variables]
    //   Margin for the Ortho Size
    private const float ORTHO_SIZE_MARGIN = 1.0f;

    // Components
    private Camera camComponent;            // SStores reference to the camera component

    //Variables for calculating camera scaling between 1 players
    private Vector3 middlePoint;            // Gets the middle point of the 2 players

    private float distanceFromMiddlePoint;   // Gets the distance from the current middle point
    private float distanceBetweenPlayers;   // Stores the distance between both players
    private float aspectRatio;              // Stores the current aspect ratio

    // Temp Variables
    private Vector3 newCameraPos;           // Stores the new camera position
	
	
	// Use this for initialization
	void Start () 
	{
		// Initializes the Camera Component
		camComponent = GetComponent<Camera>();

        // Gets the aspect ratio
        aspectRatio = Screen.width / Screen.height;
	}
	
	// Update is called once per frame

	void Update ()
    {
        // Gets the middle point of both players
        middlePoint = Ichiro.position + 0.5f * (Yumi.position - Ichiro.position);

        // Positions camea in the center of the players
        // Stores the current camera position
        newCameraPos = this.transform.position;     

        // Assigns the middle point
        newCameraPos.x = middlePoint.x;
        newCameraPos.y = middlePoint.y;

        // Sets the new camera position
        this.transform.position = newCameraPos;


        // Calculates the ortho size
        // Gets the current distance between the 2 players
        distanceBetweenPlayers = (Yumi.position - Ichiro.position).magnitude;

        // Gets the distance from the middle point
        distanceFromMiddlePoint = (this.transform.position - middlePoint).magnitude;

        // Sets the orthographic size
        camComponent.orthographicSize = Mathf.Rad2Deg * Mathf.Atan((0.5f * distanceBetweenPlayers) / (distanceFromMiddlePoint * aspectRatio)) / 15; 
        //camComponent.orthographicSize = (distanceBetweenPlayers) / (distanceFromMiddlePoint * aspectRatio) / 20;

        // Adds a small margin so the players are not on the viewport border
        //camComponent.orthographicSize += ORTHO_SIZE_MARGIN;
		
	}
}
