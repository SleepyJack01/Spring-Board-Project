using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public abstract void Interact();

    public abstract void Highlight();

    public abstract void Unhighlight();

}
