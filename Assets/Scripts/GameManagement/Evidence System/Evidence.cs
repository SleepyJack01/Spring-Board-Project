using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Evidence : MonoBehaviour
{
    public EvidenceData evidenceData;
    public bool isCollected = false;
    public bool isDeposited = false;

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }

    public void Deposit()
    {
        isDeposited = true;
    }
}
