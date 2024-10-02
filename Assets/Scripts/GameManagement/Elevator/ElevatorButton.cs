using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int floorNumber;
    private HighlightEffect highlightEffect;
    private GameObject evidenceUI;

    private void Start()
    {
        highlightEffect = GetComponent<HighlightEffect>();
        evidenceUI = transform.GetChild(0).gameObject;
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

    public void Interact()
    {
        elevator.GoToFloor(floorNumber);
    }
}
