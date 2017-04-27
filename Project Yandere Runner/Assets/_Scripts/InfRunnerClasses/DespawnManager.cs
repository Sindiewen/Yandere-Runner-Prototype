using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnManager : MonoBehaviour 
{
	// Despawns platform that exits the collider to despawn

	// public variables
	public PlatformSpawnManager platformSpawn;	// reference to the platform spawn manager script

	void OnTriggerExit2D(Collider2D col)
	{
		// If the despawn collider exits the collider of the gameobject to despawn
		if (col.gameObject.tag == "Ground")
		{
			// Destroys the game object
			Destroy(col.gameObject);

			// Decrements total number of spawned platforms
			platformSpawn._numOfTotalSpawnedPlatforms--;
			
		}

		// If a coin object exits the despawn cllider		
        if(col.gameObject.tag == "Pickup_Coin")
        {
            // destroy coin object
            Destroy(col.gameObject);
		}
	}
}
