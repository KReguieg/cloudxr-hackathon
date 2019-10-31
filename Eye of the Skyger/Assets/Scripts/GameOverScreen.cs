using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject enableOnGameOver;

    void Start()
    {
        GameManager.instance.GameOverEvent.AddListener(GameOver);
    }

    // Update is called once per frame
    void GameOver()
    {
        enableOnGameOver.SetActive(true);
    }
}
