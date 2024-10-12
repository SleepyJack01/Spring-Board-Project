using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;


public class Evidence : MonoBehaviour, IInteractable
{
    public EvidenceData evidenceData;
    private GameObject evidenceUI;

    public bool isCollected = false;
    public bool isDeposited = false;

    private void Start()
    {
        evidenceUI = transform.GetChild(0).gameObject;
    }

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }

    public void Deposit()
    {
        isDeposited = true;
    }

    public void Interact()
    {
        if (!isCollected)
        {
            Inventory.instance.AddEvidence(this);
        }
    }

    public void Highlight()
    {
        evidenceUI.SetActive(true);
    }
    

    public void Unhighlight()
    {
        evidenceUI.SetActive(false);
    }
}
