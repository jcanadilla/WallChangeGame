using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("Game").GetComponent<GameController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameController.GameOver();
        }
    }
}
