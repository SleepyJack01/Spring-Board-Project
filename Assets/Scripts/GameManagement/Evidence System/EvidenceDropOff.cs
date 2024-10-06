using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvidenceDropOff : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    public Inventory inventory;
    public Credibility credibility;
    [SerializeField] private int pointsToWin = 10;
    private int totalPoints = 0;
    //[SerializeField] private TextMeshProUGUI scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.instance == null)
            {
                Debug.LogError("Inventory.instance is null");
                return;
            }

            if (Inventory.instance.evidenceInventory == null)
            {
                Debug.LogError("Inventory.instance.evidenceInventory is null");
                return;
            }

            if (Inventory.instance.evidenceInventory.Count == 0)
            {
                //scoreText.text = "You have no evidence to deposit!";
                return;
            }

            List<Evidence> evidenceToRemove = new List<Evidence>();

            foreach (var evidence in Inventory.instance.evidenceInventory)
            {
                if (!evidence.isDeposited)
                {
                    totalPoints += evidence.evidenceData.evidencePoints;
                    evidence.Deposit();
                    evidenceToRemove.Add(evidence);
                    Debug.Log("Evidence deposited");
                }
            }

            foreach (var evidence in evidenceToRemove)
            {
                Inventory.instance.RemoveEvidence(evidence);
            }

            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        Debug.Log("Total points: " + totalPoints);
        if (totalPoints >= pointsToWin)
        {
            //scoreText.text = "You have won!";
            sceneChanger.Victory();
        }
        else
        {
            //scoreText.text = "You need " + (pointsToWin - totalPoints) + " more points to win!";
            switch (totalPoints)
            {
                case 0:
                    Debug.Log("Told Off");
                    break;

                case int i when i >= 1 && i <= 3:
                    Debug.Log("Laughed at");
                    break;

                case int i when i >= 4 && i <= 7:
                    Debug.Log("Concerned but need more evidnce");
                    break;

                case int i when i >= 8:
                    Debug.Log("Making preparations to investigate. Need a little more info"); 
                    break; 
            }
            credibility.CalculateNewCredibility(totalPoints);
            Debug.Log("New Cred: " + credibility.GetCredibility());
            if (credibility.GetCredibility() <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}