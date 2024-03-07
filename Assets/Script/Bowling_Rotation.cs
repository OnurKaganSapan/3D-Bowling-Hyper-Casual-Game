using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling_Rotation : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // D�nme h�z� (derece/saat cinsinden)

    void Update()
    {
        // Objeyi Z ekseni etraf�nda d�nd�r
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
