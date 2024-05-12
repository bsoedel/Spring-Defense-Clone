using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    private void Awake()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EndGame()
    {
        Debug.Log("end game");
    }
}
