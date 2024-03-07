using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Movement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Duvar�n hareket h�z�
    public float moveRange = 4.0f; // Duvar�n toplam hareket mesafesi
    public Material upperMaterial; // Yukar�da kullan�lacak malzeme
    public Material lowerMaterial; // A�a��da kullan�lacak malzeme
    private float initialY; // Duvar�n ba�lang�� y�ksekli�i

    public int moveDirection = 1; // Hareket y�n�: 1 yukar�, -1 a�a��
    public bool changebool = true; // Duvar�n �u anda yukar� m� a�a�� m� hareket etti�ini kontrol et
    public float growthFactor = 1.5f;
    public float degrowthFactor = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        // �arp��ma yap�lan obje bir oyuncu ise
        if (other.CompareTag("Player")) // "Player" etiketi olan bir obje ile �arp��ma kontrol�
        {
            // Oyuncuyu b�y�t
            GrowPlayer(other.transform);

            // Bu objeyi (duvar�) yok et
            Destroy(gameObject);
        }
    }

    void GrowPlayer(Transform playerTransform)
    {
        PlayerScript playerCooldown = playerTransform.GetComponent<PlayerScript>();

        // Oyuncunun boyutunu b�y�t
        Renderer renderer = GetComponent<Renderer>();
        Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();

        if (playerCooldown != null && !playerCooldown.isProtected)
        {
            playerTransform.localScale *= growthFactor;
            playerRigidbody.mass *= growthFactor; // K�tle art���n� uygula
            playerCooldown.ActivateProtection();
        }
    }
    void ActivateGrowthEffect()
    {
        // B�y�me efektini etkinle�tir
        Transform growthEffect = transform.Find("Buff"); // "B�y�meEfekti" yerine sizin efektinizin ad�n� yaz�n
        if (growthEffect != null)
        {
            growthEffect.gameObject.SetActive(true);
        }
    }

    void ActivateShrinkEffect()
    {
        // K���lme efektini etkinle�tir
        Transform shrinkEffect = transform.Find("Healing"); // "K���lmeEfekti" yerine sizin efektinizin ad�n� yaz�n
        if (shrinkEffect != null)
        {
            shrinkEffect.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // Ba�lang�� y�ksekli�ini kaydet
        initialY = transform.position.x;

    }

    void Update()
    {
        // Duvar� yukar� ve a�a�� hareket ettir
        MoveWall();
    }

    void MoveWall()
    {
        // Hareket vekt�r�
        Vector3 movement = Vector3.right * moveSpeed * moveDirection * Time.deltaTime;

        // Hareket y�n�ne g�re duvar� hareket ettir
        transform.Translate(movement);

        // Duvar�n �st veya alt s�n�r�na ula�t���nda y�n� de�i�tir
        if (transform.position.x >= initialY + moveRange / 2)
        {
            moveDirection = -1; // A�a�� hareket
        }
        else if (transform.position.x <= initialY - moveRange / 2)
        {
            moveDirection = 1; // Yukar� hareket
            changebool = !changebool;
            ChangeMaterial();
        }

        // Hareket tamamland���nda malzemeyi de�i�tir

    }

    void ChangeMaterial()
    {
        // Renderer bile�enini al
        Renderer renderer = GetComponent<Renderer>();

        // Malzemeyi de�i�tir
        if (changebool)
        {
            renderer.material = upperMaterial;
        }
        else
        {
            renderer.material = lowerMaterial;

        }
    }
}