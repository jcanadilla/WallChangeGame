using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsWindow : MonoBehaviour
{

    [SerializeField] private Text pointsText;

    public void PrintPoints(int amount) {
        pointsText.text = "Points:\n" + amount;
    }

}
