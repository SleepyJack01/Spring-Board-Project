using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPause : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public int isPaused = 0;
    void Update()
    {
        if (Input.GetKeyDown("escape") && isPaused == 0)
        {
            
            //pauseMenu.setupPauseMenu(); 
            isPaused = 1;           
        }
        else if (Input.GetKeyDown("escape") && isPaused == 1)
        {
            
            //pauseMenu.exitPauseMenu();
            isPaused = 0;
        }
    }
}