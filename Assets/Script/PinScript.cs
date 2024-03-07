using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    private bool isFallen = false;
    public float thresholdAngle = 30.0f; // Labutun düþmüþ kabul edileceði açý
    public GameDirector gameDirector; // Oyun yöneticisi scriptinin referansý

    void Update()
    {
        if (!isFallen && transform.up.y < Mathf.Cos(thresholdAngle * Mathf.Deg2Rad))
        {
            isFallen = true;
            gameDirector.PinFallen(); // Labut düþtüðünde oyun yöneticisine haber ver
        }
    }
}
