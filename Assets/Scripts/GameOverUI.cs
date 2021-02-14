using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameController gameController;
    private GameObject JumpButton;
    private Transform GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos el panel del GameOver
        GameOverPanel = this.transform.Find("GameOverPanel");
        GameOverPanel.gameObject.SetActive(false);

        // Inicializamos el boton de salto
        JumpButton = GameObject.Find("JumpButton").gameObject;

        // Inicializamos el gameController para suscribirnos al evento
        gameController = GameObject.Find("Game").GetComponent<GameController>();
        gameController.onGameOver += gameOverUIHandler;
        Debug.Log("GameOverUI subscribed to onGameOver");

        // Inicializamos el evento de gameStart
        gameController.onStartGame += gameStartUIHandler;
    }

    private void gameStartUIHandler()
    {
        GameOverPanel.gameObject.SetActive(false);
    }

    private void gameOverUIHandler()
    {
        Debug.Log("GameOverUIHandler fired!");

        // Obtenemos los puntos
        int points = gameController.GetPoints();

        // Pintamos los puntos
        GameObject PointsGO = GameOverPanel.Find("Points").gameObject;
        TextMeshProUGUI PointTextMesh = PointsGO.GetComponent<TextMeshProUGUI>();
        PointTextMesh.text = points.ToString();

        // Desactivamos el botón de salto
        JumpButton.SetActive(false);

        // Activamos el panel de Game Over
        GameOverPanel.gameObject.SetActive(true);
    }

    public void hideGameOverGUI()
    {
        GameOverPanel.gameObject.SetActive(false);
    }

}
