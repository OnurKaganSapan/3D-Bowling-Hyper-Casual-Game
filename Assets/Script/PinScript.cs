using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    private bool isFallen = false;
    public float thresholdAngle = 30.0f; // Labutun d��m�� kabul edilece�i a��
    public GameDirector gameDirector; // Oyun y�neticisi scriptinin referans�

    void Update()
    {
        if (!isFallen && transform.up.y < Mathf.Cos(thresholdAngle * Mathf.Deg2Rad))
        {
            isFallen = true;
            gameDirector.PinFallen(); // Labut d��t���nde oyun y�neticisine haber ver
        }
    }
}
