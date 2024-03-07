using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public GameDirector gameDirector;
    private TextMeshPro textMesh;
    public GameObject retryButton; // Retry butonunun referansý

    public Vector3 targetPosition; // Yeni hedef konumu

    public float moveSpeed = 1.0f; // Metnin hareket hýzý

    private bool isMoving = false;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = "Score: " + gameDirector.GetScore() + "/21";
    }
    public void ActivateRetryButton()
    {
        StartCoroutine(ActivateRetryButtonAfterDelay());
    }

    void Update()
    {
        if (gameDirector != null && textMesh != null)
        {
            textMesh.text = "Score: " + gameDirector.GetScore() + "/21";

            if (gameDirector.GetScore() > 0 && !isMoving)
            {
                isMoving = true; // Skor saymaya baþladýk ve Coroutine'i baþlatacaðýz
                StartCoroutine(MoveText());
                StartCoroutine(ActivateRetryButtonAfterDelay()); // 10 saniye sonra Retry butonunu aktif et
            }
        }
    }
    public void MoveScoreToTargetPosition()
    {
        StartCoroutine(MoveText());
    }

    public void UpdateScoreDisplay()
    {
        if (gameDirector != null)
        {
            textMesh.text = "Score: " + gameDirector.GetScore() + "/21";
        }
    }
    IEnumerator ActivateRetryButtonAfterDelay()
    {
        yield return new WaitForSeconds(3); // 10 saniye bekler
        retryButton.SetActive(true); // Retry butonunu etkinleþtir
    }
    IEnumerator MoveText()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float journeyFraction = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
            yield return null;
        }

        isMoving = false;
    }
}
