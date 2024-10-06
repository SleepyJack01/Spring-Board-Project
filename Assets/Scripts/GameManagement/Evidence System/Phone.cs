using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Phone");
    }

    public void Highlight()
    {
        Debug.Log("Highlight Phone");
    }

    public void Unhighlight()
    {
        Debug.Log("Unhighlight Phone");
    }
}
