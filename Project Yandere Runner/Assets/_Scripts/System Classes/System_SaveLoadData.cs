using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Class to save to a file
public class System_SaveLoadData : MonoBehaviour 
{

	// Public Variables

	// Classes to save data from
	[HideInInspector]
	public RNG_Seed_Generation SeedGen;	// Reference to the seed generation System

	public String SaveFileName;			// Stores the filename to save

	
	
	
	void OnApplicationQuit()
	{
		// Saves data
		SaveData();
	}


	public void SaveData()
	{

		BinaryFormatter bf = new BinaryFormatter();
		// Creates a filestream object, creates file (if none exsists)
		//	or opens file to save data to
		FileStream file = new FileStream(Application.dataPath + "/" + SaveFileName + ".yandere", FileMode.Create);

		// Creates object of class of what to save data to
		PlayerData data = new PlayerData();

		// Saves previous run date to the class
		data.PreviousRunDateDay = SeedGen.TodayDateDay;
		data.PreviousRunDateMonth = SeedGen.TodayDateMonth;
		data.PreviousRunDateYear = SeedGen.TodayDateYear;
		data.seed = SeedGen.seed;


		// Writes data to the file
		bf.Serialize(file, data);

		Debug.Log ("Saved data to file");
		Debug.Log ("Saved Date: " 
		+ data.PreviousRunDateMonth + "-" 
		+ data.PreviousRunDateDay + "-"
		+ data.PreviousRunDateYear);
		Debug.Log ("Saving Seed: " + data.seed);

		// Closes FIle
		file.Close();
		
	}
	
	public void LoadData()
	{
		BinaryFormatter bf = new BinaryFormatter();

		// Creates file stream object and open file
		FileStream file = File.Open(Application.dataPath + "/" + SaveFileName + ".yandere", FileMode.Open);

		// Stores newly loaded data to the playerdata class
		PlayerData data = (PlayerData)bf.Deserialize(file);

		// closes file
		file.Close();

		Debug.Log ("Loaded data from file");

		// Sets the newly loaded data to the coresponding class
		SeedGen.PreviousDateDay = data.PreviousRunDateDay;
		SeedGen.PreviousDateMonth = data.PreviousRunDateMonth;
		SeedGen.PreviousDateYear = data.PreviousRunDateYear;
		SeedGen.seed = data.seed;

		Debug.Log ("Loading data to file");
		Debug.Log ("Loaded Date: " 
		+ data.PreviousRunDateMonth + "-" 
		+ data.PreviousRunDateDay + "-"
		+ data.PreviousRunDateYear);
		Debug.Log ("Loading Seed: " + data.seed);
	}


}

// Class data to save to a file
[SerializableAttribute]
class PlayerData
{
	// Stores the previous run date
	public int PreviousRunDateDay;		// Stores the previous run date
	public int PreviousRunDateMonth;
	public int PreviousRunDateYear;
	
	// Stores the generated seed for spawning levels
	public int seed;
}
