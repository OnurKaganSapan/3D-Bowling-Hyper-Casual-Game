using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slider : MonoBehaviour
{

    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 rot1; // Quaternion yerine Vector3 olarak tan�mla
    public Vector3 rot2; // Quaternion yerine Vector3 olarak tan�mla
    public float speed;

    void FixedUpdate()
    {
        // Konum i�in interpolasyon
        transform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

        // Rotasyon i�in interpolasyon, Euler a��lar�n� Quaternion'a �evir
        Quaternion qRot1 = Quaternion.Euler(rot1);
        Quaternion qRot2 = Quaternion.Euler(rot2);
        transform.localRotation = Quaternion.Slerp(qRot1, qRot2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
 
}
