using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_buttons : MonoBehaviour
{

    public void play_game()
    {
        SceneManager.LoadSceneAsync("CharacterControllerTestScene");
    }

    public void quit_game()
    {
        Application.Quit();
    }
}