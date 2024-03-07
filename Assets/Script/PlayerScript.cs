using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed2 = 30.0f; // Saða sola hareket hýzý
    public float jumpForce = 10.0f; // Zýplama kuvveti
    public Transform cameraTarget; // Kameranýn takip edeceði hedef
    public LayerMask groundLayer; // Yer olarak tanýmlanan katmanlar
    public float groundCheckDistance = 0.1f; // Yer kontrolü için mesafe
    public float cameraDistance = 25.0f; // Kameranýn karaktere olan uzaklýðý
    public bool isProtected = false; // Oyuncunun korunaklý olup olmadýðýný kontrol eder
    private float cooldownTime = 0.5f; // Koruma süresi
    private float lastTapTime = 0f; // Son dokunuþun zamanýný saklar
    public float doubleTapTime = 0.5f; // Çift dokunuþ için izin verilen maksimum süre

    private Rigidbody rb; // Karakterin Rigidbody komponenti
    private bool isGameStarted = false;

    public float speedModifier = 1; // Dokunmatik hareket hýzý çarpaný
    public float forwardSpeed = 20; // Ýleri hareket hýzý
    public float maxForwardSpeed = 40f; // Maksimum ileri hýz
    public float minForwardSpeed = 10f; // Minimum ileri hýz
    public float forwardSpeedModifier = 0.5f; // Ýleri hýz deðiþim miktarý için çarpan
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        TouchInput();
        MoveForward();
        MoveCameraToTarget();
    }

    void TouchInput()
    {
        if (Input.touchCount > 0 && isGameStarted)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Çift dokunuþu kontrol et
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    // Çift dokunuþ algýlandý, zýpla
                    if (IsGrounded())
                    {
                        Jump();
                    }
                }
                // Son dokunuþ zamanýný güncelle
                lastTapTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                // Saða sola hareket
                Vector3 moveHorizontal = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime, 0, 0);
                rb.MovePosition(rb.position + moveHorizontal);
            }

            // Yukarý/aþaðý kaydýrarak hýzý ayarla
            if (Mathf.Abs(touch.deltaPosition.y) > 0)
            {
                forwardSpeed += touch.deltaPosition.y * forwardSpeedModifier * Time.deltaTime;
                forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed);
            }
        }
    }


    void MoveForward()
    {
        if (isGameStarted)
        {
            Vector3 forwardMove = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + forwardMove);
        }
    }

    void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())

        {
            Jump();
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, transform.localScale.y / 2 + groundCheckDistance, groundLayer))
        {
            return true;
        }
        return false;
    }

    void Jump()
    {
        float dynamicJumpForce = jumpForce * rb.mass;
        rb.AddForce(Vector3.up * dynamicJumpForce, ForceMode.Impulse);

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StartMovement()
    {
        isGameStarted = true;
    }

    public void ActivateProtection()
    {
        if (!isProtected)
        {
            isProtected = true;
            Invoke("DeactivateProtection", cooldownTime);
        }
    }

    void DeactivateProtection()
    {
        isProtected = false;
    }

    public float GetWeight()
    {
        return rb.mass; // Rigidbody'nin kütle deðerini döndür
    }

    void MoveCameraToTarget()
    {
        if (cameraTarget != null)
        {
            Vector3 targetPosition = cameraTarget.position - new Vector3(0, 0, cameraDistance);
            Camera.main.transform.position = targetPosition;
            Camera.main.transform.LookAt(cameraTarget.position);
        }
    }
}
