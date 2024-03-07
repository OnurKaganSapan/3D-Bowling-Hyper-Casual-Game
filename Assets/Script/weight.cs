using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weight : MonoBehaviour
{
    public PlayerScript player; // Player script'inin bir referansý
    private TextMeshPro textMesh; // TextMesh komponenti

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>(); // TextMesh komponentini al
    }

    void Update()
    {
        if (player != null)
        {
            // Player script'inden karakterin aðýrlýðýný al ve metni güncelle
            textMesh.text = (player.GetWeight() / 10f).ToString("0");

        }
    }
}
