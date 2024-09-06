using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Evidence : MonoBehaviour
{
    public EvidenceData evidenceData;
    public bool isCollected = false;

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }
}
