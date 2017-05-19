using UnityEngine;

public class Platform_Object_Spawn_Manager : MonoBehaviour 
{

	// Public Variables
	// Public Coin Spawner Variables
	[HeaderAttribute("Coin Spawn Arrtibutes")]
	public int numOfTimesCoinsCanSpawnPerPlat;	// How many coins the spawner can generate before stopping
	public Transform[] coinSpawnLocations;		// Locations for where each of the coins will spawn
	public GameObject[] coinObjects;			// the coin objects to spawn
	
	// Public Hazard Spawn Variables
	[HeaderAttribute("Hazard Spawn Attributes")]
	public bool canSpawnHazards;				// If the hazards can spawn or not when the platform is loaded
	public int NumOfTimesHazardsCanSpawnPerPlat;// How many times the hazard spawner can generate hazards before stopping
	
	[HeaderAttribute("Floating Hazards")]
	public Transform[] hazardAirSpawnLocations;	// Locations for where each floating hazard will spawn
	public GameObject[] HazardAirObjects;		// The hazard objects to spawn
	
	[HeaderAttribute("Ground Hazards")]
	public Transform[] hazardGroundSpawnLoc;	// Locations for where each ground based hazard will spawn
	public GameObject[] hazardGroundObjects;	// the Ground based hazards that will spawn 

	
	// Private Variables
	// private RNG Variables
	private int spawnFlip;						// Stores the number used by the RNG to spawn a new value
	// Private Coin Variables
	private GameObject coinClone;				// Clone of the coin Game Object
	private int totalCoinsSpawned = 0;			// Stores how many coin objects have been spawned currently
	
	// Private hazard variables
	private GameObject hazardClone;				// Clone of the hazard that will be spawned
	private int totalHazardsSpawned = 0;		// Stores how many hazards have been spawned currently





	// Use this for initialization
	void Start () 
	{
		// Initiates the corresponding functions
		SpawnCoins();				// Spawns coins
		
		if(canSpawnHazards)
		{
			SpawnFloatingHazards();		// Spawns floating hazards
			SpawnGroundHazards();		// Spawns ground based hazards
			// TODO: Have 2 spawn hazard functions
			// 1 for floating hazards, another for ground based hazards
		}
		
	}
	
	// Spawns coins
	private void SpawnCoins()
	{
		// Iterates through the array of coin spawn locations
		for (int i = 0; i < coinSpawnLocations.Length; i++)
		{
			// Generates a random number to decide weather to spawn a coin in the said location
			spawnFlip = Random.Range(0, 2);	

			if (spawnFlip > 0 && totalCoinsSpawned != numOfTimesCoinsCanSpawnPerPlat)
			{
				// Creates clone of the coin
				coinClone = coinObjects[Random.Range(0, coinObjects.Length)];	
				
				// instantiate coin in said locaation
				Instantiate(coinClone, coinSpawnLocations[i].position, Quaternion.identity);

				// Adds 1 to the totalCoinsSpawned value
				totalCoinsSpawned++;
			}
		}

	}
	
	// Spawns floating hazards at specific locations
	private void SpawnFloatingHazards()
	{
		// Loops through the floating hazard spawn locations
		for (int i = 0; i < hazardAirSpawnLocations.Length; i++)
		{
			// Generates a random numbe to decide weather to spawn a hazard in said location
			spawnFlip = Random.Range(0, 2);
			
			if (spawnFlip > 0 && totalHazardsSpawned != NumOfTimesHazardsCanSpawnPerPlat)
			{
				// creates clone of the hazard
				hazardClone = HazardAirObjects[Random.Range(0, HazardAirObjects.Length)];
				
				// Instantiates the cloned hazard
				Instantiate(hazardClone, hazardAirSpawnLocations[i].position, Quaternion.identity);
				
				// Adds 1 to the totalHazardsSpawned
				totalHazardsSpawned++;
			}
		}
	}
	
	private void SpawnGroundHazards()
	{
		// Loops through the ground hazard spawn locations
		for (int i = 0; i < hazardGroundSpawnLoc.Length; i++)
		{
			// Generates a random number to decide weather to spawn a hazard in said location
			spawnFlip = Random.Range(0, 2);			
			
			if (spawnFlip > 0 && totalHazardsSpawned != NumOfTimesHazardsCanSpawnPerPlat)
			{
				// Creates clone of the hazard
				hazardClone = hazardGroundObjects[Random.Range(0, HazardAirObjects.Length)];
				
				// Intantiates the cloned hazard
				Instantiate(hazardClone, hazardGroundSpawnLoc[i].position, Quaternion.identity);
				
				// Adds 1 to the totalHazardsSpawned count
				totalHazardsSpawned++;
			}
		}

	}

}
