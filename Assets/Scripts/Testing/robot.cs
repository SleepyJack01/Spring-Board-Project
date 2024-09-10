using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class robot : MonoBehaviour
{
    //public Text healthPanel;
    public Text batteryPanel;
    public Text credPanel;
    float battery = 90;
    float maxBattery = 90;
    //float health = 100;
    //float maxHealth = 100;
    float credibility = 0;
    float maxCredibility = 100;
    bool frozen = false;
    [SerializeField] private Slider batterySlider;
    //[SerializeField] private Slider healthSlider;
    [SerializeField] private Slider credSlider;

    // Start is called before the first frame update
    void Start()
    {
        //inflictDamage(0);     
    }

    // Update is called once per frame
    void Update()
    {
        decreaseTime();
        updateBars();
    }

    /* public void inflictDamage(int damage)
    {
        // Updates the health variable and the UI when the player takes damage.
        if (healthPanel != null && health > 0)
        {
            health = health - damage;
            healthPanel.text = "Health: " + health.ToString();
        }
    } */

    public void decreaseTime()
    {
        // Time will only decrease if it isn't frozen (can be frozen with the item in the 4th level)
        if (battery > 0 && frozen == false)
        {
            // Displays time on the UI. It is constantly updated to display an accurate time.
            battery = battery - Time.deltaTime;
            batteryPanel.text = "Battery: " + battery.ToString();
        }
        // If the time limit falls below 0, it will be set to zero so negative time isn't displayed on the screen.
        else if (battery < 0)
        {
            battery = 0;
            batteryPanel.text = "Battery: " + battery.ToString();
        }
    }

    public void updateBars()
    {
        batterySlider.value = battery / maxBattery;
        //healthSlider.value = health / maxHealth;
        credSlider.value = credibility / maxCredibility;
    }

    public void increaseCredibility(int increase)
    {  
        if (credPanel != null)
        {
            credibility = credibility + increase;
            credPanel.text = "Credibility: " + credibility.ToString();
        }
    }
}
