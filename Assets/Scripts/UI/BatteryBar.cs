using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    [SerializeField] private Image batteryImage;
    [SerializeField] private Sprite[] batterySprites;

    public void SetBattery(float battery)
    {
        battery = Mathf.Clamp(battery, 0, 100);

        
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
            batteryImage.sprite = batterySprites[3];
        }
        else if (battery > 10)
        {
            batteryImage.sprite = batterySprites[4];
        }
        else
        {
            batteryImage.sprite = batterySprites[5];
        }
    }
}