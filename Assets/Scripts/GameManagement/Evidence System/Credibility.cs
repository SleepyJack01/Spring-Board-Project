using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credibility : MonoBehaviour
{ 
    private int cred = 100;
    private int calls = 0;

    public int GetCredibility()
    {
        return cred;
    }

    public void CalculateNewCredibility(int score)
    {
        int credLoss;
        calls++;
        credLoss = (20 * calls) - (5 * score);
        if (credLoss < 10)
            credLoss = 10; 
        cred = cred - credLoss; 
    }
}
