using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence")]
public class EvidenceData : ScriptableObject
{
     public string evidenceName;
    [Range(-1, 4)]
    public int evidencePoints;
    public GameObject evidencePrefab;
}
