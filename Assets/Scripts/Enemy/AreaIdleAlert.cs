using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaIdleAlert : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameObject player;
    [SerializeField] private float timeBeforeAlert = 10f;
    private float time;
    private bool isInAlertArea = false;
    private bool isAlerted = false;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInAlertArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInAlertArea = false;
        }
    }

    private void Update()
    {
        if (isInAlertArea)
        {
            time += Time.deltaTime;
            if (time >= timeBeforeAlert && !isAlerted)
            {
                enemyController.lastKnownPosition = player.transform.position;
                enemyController.ChangeState(new AlertState(enemyController));
                isAlerted = true;
                Debug.Log("Alerted");
            }
        }
        else
        {
            time = 0;
            isAlerted = false;
        }
    }
}
