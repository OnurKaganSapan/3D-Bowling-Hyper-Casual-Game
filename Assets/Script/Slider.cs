using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slider : MonoBehaviour
{

    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 rot1; // Quaternion yerine Vector3 olarak tanýmla
    public Vector3 rot2; // Quaternion yerine Vector3 olarak tanýmla
    public float speed;

    void FixedUpdate()
    {
        // Konum için interpolasyon
        transform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

        // Rotasyon için interpolasyon, Euler açýlarýný Quaternion'a çevir
        Quaternion qRot1 = Quaternion.Euler(rot1);
        Quaternion qRot2 = Quaternion.Euler(rot2);
        transform.localRotation = Quaternion.Slerp(qRot1, qRot2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
 
}
