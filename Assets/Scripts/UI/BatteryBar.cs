using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BatteryBar : MonoBehaviour
{
    [SerializeField] private Image batteryImage;
    [SerializeField] private Sprite[] batterySprites;
    [SerializeField] private TMPro.TextMeshProUGUI batteryPercentageText;
    [SerializeField] private TMPro.TextMeshProUGUI batteryLowText;
    [SerializeField] private TMPro.TextMeshProUGUI lowBatteryWarningText;
    [SerializeField] private Volume volume;

    private bool flashing = false;
    private Coroutine flashCoroutine;
    private Vignette vignette;

    private void Start()
    {
        // Fetch the Vignette component from the Volume
        if (volume.profile.TryGet(out Vignette v))
        {
            vignette = v;
            vignette.intensity.value = 0.1f;
        }
    }

    public void SetBattery(float battery)
    {
        if (battery > 80)
        {
            batteryImage.sprite = batterySprites[0];
        }
        else if (battery > 60)
        {
            batteryImage.sprite = batterySprites[1];
        }
        else if (battery > 40)
        {
            batteryImage.sprite = batterySprites[2];
        }
        else if (battery > 20)
        {
            if (flashing && flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
                flashing = false;
                lowBatteryWarningText.enabled = false;
            }

            batteryPercentageText.enabled = false;
            batteryLowText.enabled = false;
            batteryImage.sprite = batterySprites[3];
        }
        else if (battery > 10)
        {
            
            batteryPercentageText.enabled = true;
            batteryLowText.color = new Color32(240, 209, 2, 255);
            batteryImage.sprite = batterySprites[4];

            if (!flashing && !RoboManager.isRecharging)
            {
                flashing = true;
                flashCoroutine = StartCoroutine(FlashLowWarning());
            }
        }
        else
        {
            batteryLowText.color = new Color32(227, 36, 33, 255);
            batteryImage.sprite = batterySprites[5]; 

            // Adjust vignette intensity based on the battery value
            if (vignette != null)
            {
                float intensity = Mathf.Lerp(1.0f, 0.1f, battery / 10f);
                vignette.intensity.value = intensity;
            }
        }

        batteryPercentageText.text = battery.ToString("0") + "%";
    }

    private IEnumerator FlashLowWarning()
    {
        for (int i = 0; i < 3; i++)
        {
            lowBatteryWarningText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            lowBatteryWarningText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }

        batteryLowText.enabled = true;
    }
}
