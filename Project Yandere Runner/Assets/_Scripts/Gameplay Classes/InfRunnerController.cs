using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfRunnerController : MonoBehaviour 
{

	[HeaderAttribute("Player Movement")]
	public bool playerAutoRun 		= false;		// Weather player will automatically Run
	public float maxMovementSpeed 	= 20.0f;		// The total max movement speed
	
	public float jumpForce 			= 15.0f;		// How much force should be applided for jumping
	public float fallMultiplier 	= 7.5f;			// Jumping, fall multiplier
	public float lowJumpMultiplier 	= 6.0f;			// Jumping, Low jump

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
