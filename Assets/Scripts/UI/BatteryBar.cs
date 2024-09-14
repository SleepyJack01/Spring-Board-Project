using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    [SerializeField] private Slider batteryBar;
    [SerializeField] private TMPro.TextMeshProUGUI batteryText;

    public void SetMaxBattery(float battery)
    {
        batteryBar.maxValue = battery;
        batteryBar.value = battery;
    }

    public void SetBattery(float battery)
    {
        batteryBar.value = battery;
        batteryText.text = "Battery: " + battery.ToString("0") + "%";
    }
    
}
