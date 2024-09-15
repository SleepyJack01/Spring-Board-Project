using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Evidence> evidenceInventory = new List<Evidence>();
    [SerializeField] private int maxInventorySize = 3;
    [SerializeField] private TMPro.TextMeshProUGUI evidenceCountText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        evidenceCountText.text = evidenceInventory.Count.ToString() + "/" + maxInventorySize.ToString();
    }

    public bool AddEvidence(Evidence evidence)
    {
        if (evidenceInventory.Count < maxInventorySize)
        {
            evidenceInventory.Add(evidence);
            evidence.Collect();
            evidenceCountText.text = evidenceInventory.Count.ToString() + "/" + maxInventorySize.ToString();
            return true;
        }
        else
        {
            Debug.Log("Inventory is full");
            return false;
        }
    }

    public void RemoveEvidence(Evidence evidence)
    {
        evidenceInventory.Remove(evidence);
        evidenceCountText.text = evidenceInventory.Count.ToString() + "/" + maxInventorySize.ToString();
    }
}
