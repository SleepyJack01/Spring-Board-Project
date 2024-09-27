using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Phone");
    }
}
