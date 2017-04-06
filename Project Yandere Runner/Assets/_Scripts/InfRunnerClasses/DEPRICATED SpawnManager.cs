/*
 * @author 	Rachel Vancleave
 * @date	8/26/16
 * @class	CS 214U
 * 
 * This script instantiates platforms that spawn into the scene that
 * the player can jump onto, and jump to each one individually.
 * 
 * Each platform (besides the initial start platform) is randomly placed into the scene by
 * using a new, random Vector2 location. It then places each object horizontally ahead of the last platform.
 * 
 **/

using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{

	// Public Variables -- Viewable in the inspector

	public int maxPlatforms = 20;			// The number of spawnable platforms

	// Used for spawning platforms to the right of the current platform
	public float horizontalMin 		= 6.5f;	// Minimum Horizontal distance right (+x) - Unity Units
	public float horizontalMax 		= 14f;	// Maximum Horizontal distance right (+x) - Unity Units
	public float verticalMin		= -6f;	// Minimum Vertical Distance Up (+y) - Unity Units
	public float verticalMax		= 6f;	// Maximum Vertical Distance Up (+y) - Unity Units

	public GameObject platform;				// Stores the platform GameObject

	// Private Variables

	private Vector2 originPosition;			// Stores origin position of the platform GameObject


	// Gets called at the start of the start of the game
	void Start()
	{
		// Stores the current, origin location of the GameObject into originPosition
		originPosition = transform.position;

		// Calls the Spawn function to spawn platforms
		Spawn();
	}


	void Spawn()
	{
		// If i is less than maxPlatforms, keep spawning platforms
		for (int i = 0; i < maxPlatforms; i++) 
		{
			// Creates new Vector2 variable that stores a random Vector2 location
			// Vector2(x,y)
				// X stores a random value between horizontalMin and HorizontalMax
				// Y stores a random value between verticalMin and verticalMax
			Vector2 randomPosition = originPosition + new Vector2 (Random.Range(horizontalMin, horizontalMax),
				Random.Range (verticalMin, verticalMax));

			// Instantiate returns a copy of the original object
				// Argument 1: The game object to instantiate
				// Argument 2: The location to instiantiate too
				// Argument 3: The rotation (if any, in this case none)
			Instantiate(platform, randomPosition, Quaternion.identity);

			// Stores the originPosition of the last gameObject Vector2 position
			originPosition = randomPosition; 

		}
	}
}
