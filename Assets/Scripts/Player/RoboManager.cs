using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboManager : MonoBehaviour
{
    [SerializeField] private BatteryBar batteryBar;

    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float currentBattery;
    [SerializeField] private float batteryDecreaseRate = 0.1f;

    private void Start()
    {
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
}
