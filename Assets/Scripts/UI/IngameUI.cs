using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IngameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator anim;







    public void OnTestButton()
    {
        Debug.Log("Test Button Clicked!");
    }
}

