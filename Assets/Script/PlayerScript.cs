using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed2 = 30.0f; // Sa�a sola hareket h�z�
    public float jumpForce = 10.0f; // Z�plama kuvveti
    public Transform cameraTarget; // Kameran�n takip edece�i hedef
    public LayerMask groundLayer; // Yer olarak tan�mlanan katmanlar
    public float groundCheckDistance = 0.1f; // Yer kontrol� i�in mesafe
    public float cameraDistance = 25.0f; // Kameran�n karaktere olan uzakl���
    public bool isProtected = false; // Oyuncunun korunakl� olup olmad���n� kontrol eder
    private float cooldownTime = 0.5f; // Koruma s�resi
    private float lastTapTime = 0f; // Son dokunu�un zaman�n� saklar
    public float doubleTapTime = 0.5f; // �ift dokunu� i�in izin verilen maksimum s�re

    private Rigidbody rb; // Karakterin Rigidbody komponenti
    private bool isGameStarted = false;

    public float speedModifier = 1; // Dokunmatik hareket h�z� �arpan�
    public float forwardSpeed = 20; // �leri hareket h�z�
    public float maxForwardSpeed = 40f; // Maksimum ileri h�z
    public float minForwardSpeed = 10f; // Minimum ileri h�z
    public float forwardSpeedModifier = 0.5f; // �leri h�z de�i�im miktar� i�in �arpan
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
                // �ift dokunu�u kontrol et
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    // �ift dokunu� alg�land�, z�pla
                    if (IsGrounded())
                    {
                        Jump();
                    }
                }
                // Son dokunu� zaman�n� g�ncelle
                lastTapTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                // Sa�a sola hareket
                Vector3 moveHorizontal = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime, 0, 0);
                rb.MovePosition(rb.position + moveHorizontal);
            }

            // Yukar�/a�a�� kayd�rarak h�z� ayarla
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
        return rb.mass; // Rigidbody'nin k�tle de�erini d�nd�r
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
