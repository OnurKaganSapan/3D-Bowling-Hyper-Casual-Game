using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Movement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Duvarýn hareket hýzý
    public float moveRange = 4.0f; // Duvarýn toplam hareket mesafesi
    public Material upperMaterial; // Yukarýda kullanýlacak malzeme
    public Material lowerMaterial; // Aþaðýda kullanýlacak malzeme
    private float initialY; // Duvarýn baþlangýç yüksekliði

    public int moveDirection = 1; // Hareket yönü: 1 yukarý, -1 aþaðý
    public bool changebool = true; // Duvarýn þu anda yukarý mý aþaðý mý hareket ettiðini kontrol et
    public float growthFactor = 1.5f;
    public float degrowthFactor = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        // Çarpýþma yapýlan obje bir oyuncu ise
        if (other.CompareTag("Player")) // "Player" etiketi olan bir obje ile çarpýþma kontrolü
        {
            // Oyuncuyu büyüt
            GrowPlayer(other.transform);

            // Bu objeyi (duvarý) yok et
            Destroy(gameObject);
        }
    }

    void GrowPlayer(Transform playerTransform)
    {
        PlayerScript playerCooldown = playerTransform.GetComponent<PlayerScript>();

        // Oyuncunun boyutunu büyüt
        Renderer renderer = GetComponent<Renderer>();
        Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();

        if (playerCooldown != null && !playerCooldown.isProtected)
        {
            playerTransform.localScale *= growthFactor;
            playerRigidbody.mass *= growthFactor; // Kütle artýþýný uygula
            playerCooldown.ActivateProtection();
        }
    }
    void ActivateGrowthEffect()
    {
        // Büyüme efektini etkinleþtir
        Transform growthEffect = transform.Find("Buff"); // "BüyümeEfekti" yerine sizin efektinizin adýný yazýn
        if (growthEffect != null)
        {
            growthEffect.gameObject.SetActive(true);
        }
    }

    void ActivateShrinkEffect()
    {
        // Küçülme efektini etkinleþtir
        Transform shrinkEffect = transform.Find("Healing"); // "KüçülmeEfekti" yerine sizin efektinizin adýný yazýn
        if (shrinkEffect != null)
        {
            shrinkEffect.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // Baþlangýç yüksekliðini kaydet
        initialY = transform.position.x;

    }

    void Update()
    {
        // Duvarý yukarý ve aþaðý hareket ettir
        MoveWall();
    }

    void MoveWall()
    {
        // Hareket vektörü
        Vector3 movement = Vector3.right * moveSpeed * moveDirection * Time.deltaTime;

        // Hareket yönüne göre duvarý hareket ettir
        transform.Translate(movement);

        // Duvarýn üst veya alt sýnýrýna ulaþtýðýnda yönü deðiþtir
        if (transform.position.x >= initialY + moveRange / 2)
        {
            moveDirection = -1; // Aþaðý hareket
        }
        else if (transform.position.x <= initialY - moveRange / 2)
        {
            moveDirection = 1; // Yukarý hareket
            changebool = !changebool;
            ChangeMaterial();
        }

        // Hareket tamamlandýðýnda malzemeyi deðiþtir

    }

    void ChangeMaterial()
    {
        // Renderer bileþenini al
        Renderer renderer = GetComponent<Renderer>();

        // Malzemeyi deðiþtir
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