using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Controller : MonoBehaviour
{
	// public variables
	//	Players
	[HeaderAttribute("Players")]
	public Transform Ichiro;		// Reference to the Ichiro Player
	public Transform Yumi;			// Reference to the Yumi Player
	
	// Private Variables
	private Camera camComponent;		// SStores reference to the camera component
	
	
	
	
	
	// Use this for initialization
	void Start () 
	{
		// Initializes the Camera Component
		camComponent = GetComponent<Camera>();	
		
		camComponent.orthographicSize = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
