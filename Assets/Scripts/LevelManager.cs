using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public GameObject canvas;
    private bool gameIsOver = false;
    private void Awake()
    {
        main = this;
    }

    void Update()
    {
        if (!gameIsOver) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void EndGame()
    {
        canvas.SetActive(true);
        gameIsOver = true;
    }
}
