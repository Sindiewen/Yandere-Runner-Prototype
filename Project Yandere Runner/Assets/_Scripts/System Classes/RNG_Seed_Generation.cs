using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.DateTime.Now;
using UnityEngine;


public class RNG_Seed_Generation : MonoBehaviour 
{

	// private Variables
	private int TodayDateDay;	// Stores todays date - todays
	private int PreviousDateDay;	// Stores the previous date - last time game was run


	private int HourToSeed = 21;		// Sores an int for when the game will seed a value - Hour
	private int MinToSeed;		// Minuites
	private int SecToSeed;		// Seconds
	




	// Current DateTime
	int CurrentHour = System.DateTime.Now.Hour;
	int currentMin = System.DateTime.Now.Minute;
	int currentSec = System.DateTime.Now.Second;
	
	
	
	

	void Start()
	{
		SeedRandomNumberByTime();
	}

	
	public void SeedRandomNumberByTime()
	{
		if(HourToSeed == CurrentHour)
		{
			Debug.Log("It's 9PM!");
		}
	}


}
