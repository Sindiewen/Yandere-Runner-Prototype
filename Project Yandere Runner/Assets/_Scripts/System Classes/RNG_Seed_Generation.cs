using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RNG_Seed_Generation : MonoBehaviour 
{

	// Class to save data to
	public System_SaveLoadData saveload;


	[HeaderAttribute("Today's Date")]
	// Public Variables
	public bool DateDebugMode;	// Toggle to allow the date to be debugged
	public int TodayDateDay;	// Stores todays date - Day of Week
	public int TodayDateMonth;	// Stores todays date - Month 
	public int TodayDateYear;	// Stores todays date - year
	
	
	[HeaderAttribute ("Previous Opened Date")]
	public int PreviousDateDay;		// Stores the previous date - last time game was run
	public int PreviousDateMonth;	// Month
	public int PreviousDateYear;	// Year

	[HeaderAttribute ("Current Seed")]
	public int seed;
		

	void Start()
	{
		// If we're not debugging the date
		if (!DateDebugMode)
		{
			// Initialies todays date by the current system date and time
			TodayDateDay = System.DateTime.Now.Day;	// Initializes todays date - day
			TodayDateMonth = System.DateTime.Now.Month;	
			TodayDateYear = System.DateTime.Now.Year;
		}
		// Else - Will pull values set in the inspector


		// Initializes last load date
		//	Loads previous day, month and year
		saveload.LoadData();


		// Seeds random level if the day is different
		SeedRandomNumberByDate();

		
		}

	
	public int SeedRandomNumberByDate()
	{
		// If previous run date is == to todays run date
		if (TodayDateDay != PreviousDateDay && TodayDateMonth != PreviousDateMonth && TodayDateYear != PreviousDateYear)
		{
			Debug.Log("Todays date doesnt match last opened date. Seeding new level");
			// Seeds new number for random number generation
		}
		else
		{
			Debug.Log("Game opened on the same day. No need to re-seed a level");
		}

		seed = 1;

		Debug.Log("Seeded Number: " + seed);
		return seed;
	}
}
