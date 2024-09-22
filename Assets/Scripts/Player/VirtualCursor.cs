using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VirtualCursor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    private PointerEventData pointerEventData;
    private GameObject lastHoveredObject;

    private void Update()
    {
        HandleUIRaycast();
    }

    private void HandleUIRaycast()
    {
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = new Vector2(Screen.width / 2, Screen.height / 2)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        GameObject uiElement = null;

        foreach (var result in results)
        {
            var selectable = result.gameObject.GetComponentInParent<Selectable>();
            if (selectable != null && selectable.IsInteractable())
            {
                uiElement = selectable.gameObject;
                break;
            }
        }

        if (uiElement != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ExecuteEvents.Execute(uiElement, pointerEventData, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(uiElement, pointerEventData, ExecuteEvents.pointerClickHandler);
                ExecuteEvents.Execute(uiElement, pointerEventData, ExecuteEvents.pointerUpHandler);
            }

            if (uiElement != lastHoveredObject)
            {
                if (lastHoveredObject != null)
                {
                    ExecuteEvents.Execute(lastHoveredObject, pointerEventData, ExecuteEvents.pointerExitHandler);
                }

                ExecuteEvents.Execute(uiElement, pointerEventData, ExecuteEvents.pointerEnterHandler);
                lastHoveredObject = uiElement;
            }
        }
        else if (lastHoveredObject != null)
        {
            ExecuteEvents.Execute(lastHoveredObject, pointerEventData, ExecuteEvents.pointerExitHandler);
            lastHoveredObject = null;
        }
    }
}