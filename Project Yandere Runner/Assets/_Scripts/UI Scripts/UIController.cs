using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public IchiroDistanceController ichiro;     // Reference to the Ichiro Controller

    [HeaderAttribute("Distance Traveled")]
    public Text distanceTraveledText;   // Reference to the distance traveled text




    private void Update()
    {
        // Updated the distance traveled text
        distanceTraveledText.text = ichiro.distanceTraveled.ToString();
    }




}
