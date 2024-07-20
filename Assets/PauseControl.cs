using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseControl : MonoBehaviour{

    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused =! gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame ()
    {
        if (gameIsPaused)
        {
			MessageScript.Message = "GAME PAUSED!";
            Time.timeScale = 0f;
        }
        else 
        {
			MessageScript.Message="";
            Time.timeScale = 1;
        }
    }
}