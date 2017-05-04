using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchiroPowerupController : MonoBehaviour 
{
	// Public Variables
	[HeaderAttribute ("UI")]
	public UIController UI;							// Reference to the UI Controller


	[HeaderAttribute ("Coin Speed Boost")]
	public int speedBoostTime;						// How long the speed boost will last
	public int speedBoosIncrease;					// How much the player speed will increase
	public int numberOfCoinsRequired;				// Stores the number of coins required before the player will do a speed boost
	public int coinCount = 0;						// Initiallizes the current coin count. Counts number of coins the player picked up
	public int coinCountInrow = 0;					// How many coins the player picked up in a row

	// Private Variables
	private IchiroController ichiroController;		// Initializes the Ichiro player controller

	// Use this for initialization
	void Start () 
	{
		// Creates reference to the ichiroController
		ichiroController = GetComponent<IchiroController>();	
	}
	
	 // If the player collides with a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collides with a coin object via Tag
        if(collision.gameObject.tag == "Pickup_Coin")
        {
            // destroy coin object
            Destroy(collision.gameObject);

            // Add 1 to total coin count
            coinCount++;
			coinCountInrow++;
            
			// Adds 1 to the UI Coin Count Text
			UI.IterateCoinCount(coinCount);	

            //Debug.Log("Total Coin Count: " + UI.coinCount.ToString());
            
			// If the user picked up a total of 10 coins in a row
			if (coinCountInrow == numberOfCoinsRequired)
			{
				// reset coin coint in row back to 0
				coinCountInrow = 0;

				// Call IEnumerator coroutine
				StartCoroutine("tempSpeedBoost");	
			}


        }
    } 

	private IEnumerator tempSpeedBoost()
	{
		// Grants a temp speed boost
		
		ichiroController.maxMovementSpeed += speedBoosIncrease;

		yield return new WaitForSeconds(speedBoostTime);
		
		// rmoves temp speed boost
		ichiroController.maxMovementSpeed -= speedBoosIncrease;


	}
}
