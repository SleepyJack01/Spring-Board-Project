using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUi : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Start()
    {
        player = GameObject.Find("RoboDog");
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
    }
}
