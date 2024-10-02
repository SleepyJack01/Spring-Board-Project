using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera roboCamera;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private GameObject detectedObject;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(roboCamera.transform.position, roboCamera.transform.forward, out hit, interactionDistance))
        {
            HandleRaycastHit(hit);
        }
        else
        {
            ClearDetectedObject();
        }
    }

     private void HandleRaycastHit(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Interactable"))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Highlight();
                detectedObject = hit.collider.gameObject;
            }
        }
        else
        {
            ClearDetectedObject();
        }
    }

    private void ClearDetectedObject()
    {
        if (detectedObject != null)
        {
            IInteractable interactable = detectedObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Unhighlight();
            }
            detectedObject = null;
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(roboCamera.transform.position, roboCamera.transform.forward, out hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
