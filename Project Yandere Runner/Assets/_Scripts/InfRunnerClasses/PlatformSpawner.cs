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
	[Header ("Platform Spawner Atributes")]
	public GameObject[] PlatformHolder;		// Holds the platforms for the game to spawn in

	public int numOfPlatformsToSpawn;		// Stores an int for how many plaatforms to spawn into the world

	[Header ("Horizontal Platform Spawn Buffer")]
	public int minPlatformSpawnBuffer = 0;		// Minimum platform spawn buffer between platforms
	public int maxPlatformSpawnBuffer = 10;		// Maximum platform spawn buffer between platforms
	
	[Header ("Vertical Platform Spawn Buffer")]
	public int minPlatformSpawnHeight = -5;		// minimum height platforms will spawn
	public int maxPlatformSpawnHeight = 5;		// Maximum height platforms will spawn

	[HeaderAttribute ("Random Number Generation by Seed")]
	//public RNG_Seed_Generation seed;



	// Private Variables //

	private GameObject _WhatIsCurrentPlatform;			// Stores the current platform
	private GameObject _WhatIsCurrentPlatformClone;		// Clone of the current platform
	private GameObject _WhatIsNextPlatform;				// Stores the next platform to spawn
	private GameObject _WhatIsNextPlatformClone;		// Clone of the current platform;


	private Vector2 _CurPlatformPosition;				// Stores the current platform location
	private float _CurPlatformHalfWidth;				// Stores the current platforms half of the width 
	
	private float _NextPlatformHalfWidth;				// Stores the next platforms half of the width
	private Vector2 _NextPlatformNewSpawnLocation;		// Stores a location for the next platfor to spawn to

	
	private float _TotalPlatformWidths;					// Stores the toral platform Widths for both the current and next platform
	private int _PlatformSpawnBuffer;					// The bufferm distance the platforms will spawn away from each other
	private int _PlatformSpawnVertBuffer;				// The Buffer of how high the platforms can spawn
	private int _RandomPlatformToSpawn;					// Stores platform to spawn

	// Use this for initialization
	void Start ()
	{
		// Initializes the start platform
		SpawnFirstPlatform();

		// Spawns 20 platforms initially
		for(int i = 0; i < numOfPlatformsToSpawn; i++)
		{
			SpawnPlatforms();
		}


	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	


	// Platform Spawner Functions //

	// Spawns the first platform
	void SpawnFirstPlatform()
	{
		// Creates an origin point for the initial platform to start at.
		Vector2 Origin = new Vector2 (0,0);			
	
		// Initiates a current platform
		_WhatIsCurrentPlatform = PlatformHolder[0];				// Gets the current platform from the top of the platform holder
		_WhatIsCurrentPlatformClone = _WhatIsCurrentPlatform;	// Stores a clone of the current platform 
		

		// Spawn the platform at origin - 0,0
		Instantiate(_WhatIsCurrentPlatformClone, Origin, Quaternion.identity);
	}

	void SpawnPlatforms()
	{
		// Seeds random value
		//Random.InitState(seed.SeedRandomNumberByDate());
		
		// Random Number Generation determining platform Spawning  //
		//		Seeds random value for determining what platform to spawn next
		_RandomPlatformToSpawn = Random.Range(0, PlatformHolder.Length);
		
		// 		Seeds random number to create a buffer between both platforms
		_PlatformSpawnBuffer = Random.Range(minPlatformSpawnBuffer, maxPlatformSpawnBuffer);

		//		Seeds random value to determine how high the platforms will spawn'
		_PlatformSpawnVertBuffer = Random.Range(minPlatformSpawnHeight, maxPlatformSpawnHeight);		

		
	
	
		// Platform Spawning Logic
		// Store the current platform location in 3D Space
		_CurPlatformPosition = _WhatIsCurrentPlatform.transform.position;
		
		// Get Half of the width of the current platform
		_CurPlatformHalfWidth = _WhatIsCurrentPlatform.transform.localScale.x / 2;

		// Loads next platform into what's next
		//		Determined by a random number
		_WhatIsNextPlatform = PlatformHolder[_RandomPlatformToSpawn];

		// Get next platforms half of the width
		_NextPlatformHalfWidth = _WhatIsNextPlatform.transform.localScale.x / 2;

		// math to determine the distance of where to place the newly generated platforms //
		//		By using half of the width of the current and next platform, this will generate the minimum distance
		//		the platforms can spawn. Thus allowing a buffer in between the platforms to allow gaps in between
		//		without risk of the platforms overlaping.
		_TotalPlatformWidths = _CurPlatformHalfWidth + _NextPlatformHalfWidth + _PlatformSpawnBuffer;


		// Initiates platform spawning into the game scene	
		// Creates a new vector2 position for the platform to spawn to
		_NextPlatformNewSpawnLocation += new Vector2 (_CurPlatformPosition.x + _TotalPlatformWidths, _PlatformSpawnVertBuffer);

		// Sets the newly spawned platform as the current platform
		_WhatIsCurrentPlatform = _WhatIsNextPlatform;

		// Instantiate's the new platform
		Instantiate(_WhatIsNextPlatform, _NextPlatformNewSpawnLocation, Quaternion.identity);		
	}


}


