using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencePickup : MonoBehaviour
{
    private Inventory inventory;

    private void Start() 
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Evidence"))
        {
            Evidence evidence = other.gameObject.GetComponent<Evidence>();
            if (evidence != null && !evidence.isCollected)
            {
                inventory.AddEvidence(evidence);
            }
        }
    }
}
