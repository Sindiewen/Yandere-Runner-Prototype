using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Variables
    [HeaderAttribute("Distance Traveled")]
    public Text distanceTraveledText;           // Reference to the distance traveled text

    [HeaderAttribute("Coins")]
    public Text coinCountText;                  // Reference to the Coin Count Text
    




    public void UpdateDistance(float distanceTraveled)
    {
        // Updated the distance traveled text
        //distanceTraveledText.text = Mathf.RoundToInt(ichiroDistance.distanceTraveled).ToString();
        distanceTraveledText.text = Mathf.RoundToInt(distanceTraveled).ToString();
    }

    // passes in the current coin count
    public void IterateCoinCount(int coinCount)
    {
        // Updates the current coin count
        coinCountText.text = coinCount.ToString();
    }




}
