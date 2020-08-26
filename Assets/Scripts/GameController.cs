using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private PointsWindow pointsWindow;
    private int points = 1;

    // Update is called once per frame
    void Update()
    {
        this.IncreasePoints();
        this.pointsWindow.PrintPoints(this.points);
    }

    private void IncreasePoints()
    {
        points += 1 + (int)(0.5f * Time.time);
    }

}
