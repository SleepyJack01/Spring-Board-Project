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
    private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    [Header("Trigger Settings")]
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

    RaycastHit hit; 

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

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

        audioSource.PlayOneShot(hitSound);
    }

    // public bool CanSeeEnemy()
    // {
    //     Physics.Raycast(transform.position, transform.forward, out hit);
    //     if (hit.collider == null)
    //     {
    //         return false;
    //     }
    //     if (hit.collider.gameObject.CompareTag("Enemy"))
    //     {
    //         //Debug.Log("Enemy Seen");
    //         return true;
    //     }
    //     return false;
    // }

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.tag)
        {
            case "Charger":
                isRecharging = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        switch (other.tag)
        {
            case "Charger":
                isRecharging = false;
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

    public void OnNightVision(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            VolumeProfile profile = volume.profile;
            if (profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.active = !colorAdjustments.active;
            }
            if (profile.TryGet(out ShadowsMidtonesHighlights shadowsMidtonesHighlights))
            {
                shadowsMidtonesHighlights.active = !shadowsMidtonesHighlights.active;
            }
        }
    }
}
