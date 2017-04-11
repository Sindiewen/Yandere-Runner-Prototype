using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiContoller : MonoBehaviour 
{

	// Public Variables

	[HeaderAttribute ("Wall Values")]
	public float wallDistance;		// How far Yumi is from a wall 
	public LayerMask whatIsWall;	// Choses what the wall is so yumi knows when to jump

	public float jumpPointDistance;		// How far Yumi is from a jump point
	public LayerMask whatIsJumpPoint;	// A location where yumi must jump before 

	// Private Variables //

	// Where the raycast will start
	private Vector2 _inFrontOfYumi;
	
	// Raycast
	private RaycastHit2D _hitWall2D;
	private RaycastHit2D _hitJumpPoint2D;
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Defines raycast values for wall and Jump point
		//	This transform's location, where, distance to wall/Jump Point, and what is defined as a wall/Jump Point
		_hitWall2D 		= Physics2D.Raycast(this.transform.position, _inFrontOfYumi, wallDistance, whatIsWall);
		_hitJumpPoint2D = Physics2D.Raycast(this.transform.position, _inFrontOfYumi, jumpPointDistance, whatIsJumpPoint);
		
		// Sets the length of the raycast
		_inFrontOfYumi = Vector2.right;
		
		// Checks if the raycast has hit a wall or a Jump Point
		if (_hitWall2D || _hitJumpPoint2D)
		{
			// Print to console yumi has hit a wall
			Debug.Log(this.transform.name + " is in front of " + _hitWall2D.collider.gameObject.name);
			Debug.Log(this.transform.name + " is in front of " + _hitJumpPoint2D.collider.gameObject.name);
		}
	 
		
	}
}
