using UnityEngine;

public class CoinSpawner : MonoBehaviour 
{

	// Public Variables
	[HeaderAttribute("Coin Spawn Arrtibutes")]
	public Transform[] coinSpawnLocations;		// Locations for where each of the coins will spawn
	public GameObject[] coinObjects;			// the coin to spawn
	
	public int numOfCoinsCanSpawn;				// How many coins the spawner can generate before stopping
	
	// Private Variables
	private GameObject coinClone;				// Clone of the coin Game Object
	private int coinFlip;						// Stores a value that will randomly generate a coin in the spawn location





	// Use this for initialization
	void Start () 
	{
		// Initiates the spawn coin script
		SpawnCoins();
		
	}
	
	// Spawns coins
	private void SpawnCoins()
	{
		// Iterates through the array of coin spawn locations
		for (int i = 0, totalCoinsSpawned = 0; i < coinSpawnLocations.Length; i++)
		{
			coinFlip = Random.Range(0, 2);	// Generates a random number to decide weather to spawn a coin in the said location

			if (coinFlip > 0 && totalCoinsSpawned != numOfCoinsCanSpawn)
			{
				// Creates clone of the coin
				coinClone = coinObjects[Random.Range(0, coinObjects.Length)];	
				
				// instantiate coin in said locaation
				Instantiate(coinClone, coinSpawnLocations[i].position, Quaternion.identity);

				// Adds 1 to the coinsCanSpawn value
				totalCoinsSpawned++;
			}
		}

	}

}
