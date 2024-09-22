using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboManager : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private Transform chargerPos;

    [Header("Battery Settings")]
    [SerializeField] private BatteryBar batteryBar;

    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float currentBattery;
    [SerializeField] private float batteryDecreaseRate = 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();

        currentBattery = maxBattery;
        batteryBar.SetMaxBattery(maxBattery);
        batteryBar.SetBattery(currentBattery);
    }

    private void Update()
    {
        currentBattery -= batteryDecreaseRate * Time.deltaTime;
        batteryBar.SetBattery(currentBattery);
    }

    public void RobotHit(float damage)
    {
        currentBattery -= damage;

        batteryBar.SetBattery(currentBattery);
    }

    private void OnTriggerEnter(Collider other) 
    {
        //triggers for certain collisons in the game world
        switch (other.tag)
        {
            case "VanEnter":
                StartCoroutine(FadeOut());
                break;
        }
    }

    private IEnumerator FadeOut()
    {
        playerMovement.enabled = false;
        controller.enabled = false;

        yield return new WaitForSeconds(1.8f);
        
        transform.position = chargerPos.position;
        transform.rotation = chargerPos.rotation;
    }
}
