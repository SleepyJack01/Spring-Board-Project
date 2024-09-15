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
            if (hit.collider.CompareTag("Evidence"))
            {
                // Activate UI object on object
                hit.collider.GetComponent<Evidence>().ActivateUI();
                detectedObject = hit.collider.gameObject;
            }
            else if (detectedObject != null)
            {
                detectedObject.GetComponent<Evidence>().DeactivateUI();
                detectedObject = null;
            }
        }
        else if (detectedObject != null)
        {
            detectedObject.GetComponent<Evidence>().DeactivateUI();
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
