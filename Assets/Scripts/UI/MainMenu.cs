using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SceneChanger sceneChanger;

    [Header("First Selected Setup")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstSelected;

    private void Awake()
    {
        sceneChanger = GetComponent<SceneChanger>();
        eventSystem = FindObjectOfType<EventSystem>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void StartGame()
    {
        sceneChanger.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
