using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerLook playerLook;

    [Header("First Selected Setup")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstSelected;

    

    public static bool gameIsPaused = false;

    private void Awake()
    {
        playerLook = FindObjectOfType<PlayerLook>();
        eventSystem = FindObjectOfType<EventSystem>();
        pauseMenu.SetActive(false);
    }

    public void OnControlSchemeChanged()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            eventSystem.SetSelectedGameObject(firstSelected);
        }
        else
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    public void PauseGame()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            eventSystem.SetSelectedGameObject(firstSelected);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        playerLook.enabled = false;

        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(null);

        playerLook.enabled = true;

        gameIsPaused = false;
    }

    public void QuitToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    

    public void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}