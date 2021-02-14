using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Estado de la partida
    public delegate void OnGameOver();
    public event OnGameOver onGameOver;

    public delegate void OnStartGame();
    public event OnStartGame onStartGame;

    // Puntos
    [SerializeField] private PointsWindow pointsWindow;
    private int points = 1;

    // Time inicial
    float initialTime;

    // Elementos del juego
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftWalls;
    [SerializeField] private GameObject rightWalls;
    [SerializeField] private GameObject sections;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject jumpButton;

    private void Start()
    {
        onGameOver += GameController_onGameOver;
        initialTime = Time.time;
    }

    private void GameController_onGameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
        pointsWindow.SetActive(false);
    }

    public void GameOver()
    {
        this.onGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        this.IncreasePoints();
        this.pointsWindow.PrintPoints(this.points);
    }

    private void IncreasePoints()
    {
        if (Time.timeScale > 0)
        {
            points += 1 + (int)(0.2f * (Time.time - initialTime));
        }
    }

    public int GetPoints()
    {
        return this.points;
    }

    public void RestartGame()
    {
        mainCamera.transform.position = new Vector3(0, 0, -12);
        
        player.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().Initialize();
        
        leftWalls.transform.position = new Vector3(-3.5f, 0, 0);
        leftWalls.GetComponent<WallController>().Restart();
        
        rightWalls.transform.position = new Vector3(3.5f, 0, 0);
        rightWalls.GetComponent<WallController>().Restart();

        sections.transform.position = new Vector3(0, 0, 0);
        sections.GetComponent<SectionController>().Restart();

        points = 0;
        pointsWindow.SetActive(true);

        jumpButton.SetActive(true);

        Time.timeScale = 1;
        initialTime = Time.time;
        onStartGame();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
