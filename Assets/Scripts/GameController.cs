using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Estado de la partida
    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;

    // Puntos
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
        points += 1 + (int)(0.2f * Time.time);
    }

}
