using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RoboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Volume volume;

    [Header("Trigger Settings")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private Transform chargerPos;

    [Header("Battery Settings")]
    [SerializeField] private BatteryBar batteryBar;

    [SerializeField] private float maxBattery = 100f;
    [Range(0, 100)]
    [SerializeField] private float currentBattery;
    [SerializeField] private float batteryDecreaseRate = 0.1f;
    [SerializeField] private float batteryIncreaseRate = 4f;
    [SerializeField] private TMPro.TextMeshProUGUI rechargeText;
    public static bool isRecharging = false;
    private bool textIsFlashing = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();

        currentBattery = maxBattery;
        batteryBar.SetBattery(currentBattery);
    }

    private void Update()
    {
        if (!isRecharging)
        {
            BatteryDrain();
            StopCoroutine(FlashRechargeText());
        }
        else
        {
            RechargeBattery();
        }

    }

    private void BatteryDrain()
    {
        currentBattery -= batteryDecreaseRate * Time.deltaTime;
        batteryBar.SetBattery(currentBattery);

        if (currentBattery <= 0)
        {
            sceneChanger.GameOver();
        }
    }

    private void RechargeBattery()
    {
        currentBattery += batteryIncreaseRate * Time.deltaTime;
        batteryBar.SetBattery(currentBattery);
        if (!textIsFlashing)
        {
            StartCoroutine(FlashRechargeText());
        }
        

        if (currentBattery >= maxBattery)
        {
            isRecharging = false;
        }   
    }

    public void RobotHit(float damage)
    {
        currentBattery -= damage;

        batteryBar.SetBattery(currentBattery);
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.tag)
        {
            case "VanEnter":
                StartCoroutine(FadeOut());
                break;
            case "Charger":
                isRecharging = true;
                break;
        }
    }

    private IEnumerator FlashRechargeText()
    {
        textIsFlashing = true;
        rechargeText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        rechargeText.enabled = false;
        yield return new WaitForSeconds(0.5f);
        textIsFlashing = false;
    }

    private IEnumerator FadeOut()
    {
        playerMovement.enabled = false;
        controller.enabled = false;

        yield return new WaitForSeconds(1.8f);
        
        transform.position = chargerPos.position;
        transform.rotation = chargerPos.rotation;
    }

    public void OnNightVision(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            VolumeProfile profile = volume.profile;
            if (profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.active = !colorAdjustments.active;
            }
        }
    }
}
