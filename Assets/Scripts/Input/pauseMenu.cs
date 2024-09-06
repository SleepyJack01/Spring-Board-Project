using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public void setupPauseMenu()
    {
        //Sets the pause menu as active so it is displayed
        gameObject.SetActive(true);
        // Freezes time in the game 
        Time.timeScale = 0;       
    }
    public void exitPauseMenu()
    {
        // Sets the pause menu back to false so it is hidden again.
        gameObject.SetActive(false);
        //Unfreezes time
        Time.timeScale = 1;
    }
}