using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;


public class Evidence : MonoBehaviour, IInteractable
{
    public EvidenceData evidenceData;
    private GameObject evidenceUI;
    private HighlightEffect highlightEffect;

    public bool isCollected = false;
    public bool isDeposited = false;

    private void Start()
    {
        highlightEffect = GetComponent<HighlightEffect>();
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
        highlightEffect.SetHighlighted(true);
        evidenceUI.SetActive(true);
    }
    

    public void Unhighlight()
    {
        highlightEffect.SetHighlighted(false);
        evidenceUI.SetActive(false);
    }
}
