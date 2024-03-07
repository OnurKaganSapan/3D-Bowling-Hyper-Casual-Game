using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling_Rotation : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Dönme hýzý (derece/saat cinsinden)

    void Update()
    {
        // Objeyi Z ekseni etrafýnda döndür
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
