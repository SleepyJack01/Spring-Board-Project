using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int floorNumber;

    public void Interact()
    {
        elevator.GoToFloor(floorNumber);
    }
}
