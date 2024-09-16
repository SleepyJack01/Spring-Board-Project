using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject roboDog;
    [SerializeField] private Animator elevatorAnimator;

    [SerializeField] private Transform[] floorSpawnPoints;
    private bool isTransitioning = false;

    public void GoToFloor(int floorNumber)
    {

        if (floorNumber >= 0 && floorNumber < floorSpawnPoints.Length && !isTransitioning)
        {
            StartCoroutine(MoveToFloor(floorNumber));
        }
        
    }

    IEnumerator MoveToFloor(int floorNumber)
    {
        isTransitioning = true;

        roboDog.GetComponent<PlayerMovement>().enabled = false;
        roboDog.GetComponent<CharacterController>().enabled = false;

        elevatorAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1.5f);

        elevatorAnimator.SetTrigger("FadeIn");
        
        roboDog.transform.position = floorSpawnPoints[floorNumber].position;
        roboDog.transform.rotation = floorSpawnPoints[floorNumber].rotation;

        roboDog.GetComponent<PlayerMovement>().enabled = true;
        roboDog.GetComponent<CharacterController>().enabled = true;

        isTransitioning = false;
    }
}
