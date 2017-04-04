using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// summary
/*
 * Attach to a singletons object to spawn platform
 */

public class PlatformSpawner : MonoBehaviour
{
	// Public Variables
	public GameObject[] PlatformHolder;		// Holds the platforms for the game to spawn in


	// Private Variables //

	private GameObject _WhatIsCurrentPlatform;	// Stores the current platform
	private GameObject _WhatIsCurrentPlatformClone;	// Clone of the current platform
	private GameObject _WhatIsNextPlatform;		// Stores the next platform to spawn
	private GameObject _WhatIsNextPlatformClone;	// Clone of the current platform;
	
	private Vector2 _CurPlatformPosition;		// Stores the current platform location
	private float _CurPlatformHalfWidth;		// Stores the current platforms half of the width 
	
	private float _NextPlatformHalfWidth;		// Stores the next platforms half of the width
	private Vector2 _NextPlatformNewSpawnLocation;	// Stores a location for the next platfor to spawn to
	
	private float _TotalPlatformWidths;				// Stores the toral platform Widths for both the current and next platform
	private int _PlatformSpawnBuffer;				// The bufferm distance the platforms will spawn away from each other
	private int _RandomPlatformToSpawn;				// Stores platform to spawn

	// Use this for initialization
	void Start ()
	{
		

		// Stores the current platform
		_WhatIsCurrentPlatform = PlatformHolder[0];	// Gets the current platform from the top of the platform holder
		_WhatIsCurrentPlatformClone = _WhatIsCurrentPlatform;	// Stores a clone of the current platform 
		
		Vector2 Origin = new Vector2 (0,0);

		// Spawn the platform at origin - 0,0
		Instantiate(_WhatIsCurrentPlatformClone, Origin, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			// Store the current platform location in 3D Space
			_CurPlatformPosition = _WhatIsCurrentPlatform.transform.position;
		
			// Get Half of the width of the current platform
			_CurPlatformHalfWidth = _WhatIsCurrentPlatform.transform.localScale.x / 2;
			//Debug.Log("Current Platform Half Width: " + _CurPlatformHalfWidth);
			
			// Stores a random number to spawn a platform from
			_RandomPlatformToSpawn = Random.Range(0, PlatformHolder.Length);
			
			// Loads next platform into the holder	- The 2nd platform in the list
			_WhatIsNextPlatform = PlatformHolder[_RandomPlatformToSpawn];

			// Get next platforms half width
			_NextPlatformHalfWidth = _WhatIsNextPlatform.transform.localScale.x / 2;

			// Seeds random number to create a buffer between both platforms
			_PlatformSpawnBuffer = Random.Range(1, 10);
			//Debug.Log ("Platform Spawn Buffer: " + _PlatformSpawnBuffer);
			
			// Adds both of the platform Widths and the platform buffer
			_TotalPlatformWidths = _CurPlatformHalfWidth + _NextPlatformHalfWidth + _PlatformSpawnBuffer;

			// Creates a new vector2 position for the platform to spawn to
			_NextPlatformNewSpawnLocation += new Vector2 (_CurPlatformPosition.x + _TotalPlatformWidths, 0);

			// Sets the newly spawned platform as the current platform
			_WhatIsCurrentPlatform = _WhatIsNextPlatform;

			// Instantiate's the new platform
			Instantiate(_WhatIsNextPlatform, _NextPlatformNewSpawnLocation, Quaternion.identity);

		}


	}
}


