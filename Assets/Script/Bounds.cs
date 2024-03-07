using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Transform VectorBack;
    public Transform VectorForward;
    public Transform VectorRight;
    public Transform VectorLeft;
    public ScoreDisplay scoreDisplay; // ScoreDisplay referansý

    public void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, VectorLeft.transform.position.x, VectorRight.transform.position.x);
        transform.position = viewPos;
        ControlCamera();
    }
    void ControlCamera()
    {
        // Kameranýn mevcut ve hedef pozisyonlarýný al
        float cameraZ = Camera.main.transform.position.z;
        float targetZ = Mathf.Min(transform.position.z - 25 , VectorForward.transform.position.z);
        
        // Kamera pozisyonunu güncelle
        Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, targetZ);
        Camera.main.transform.position = targetPosition;
        if (Camera.main.transform.position.z >= VectorForward.transform.position.z)
        {
            scoreDisplay.ActivateRetryButton();
            scoreDisplay.UpdateScoreDisplay();
            scoreDisplay.MoveScoreToTargetPosition();
        }
    }

}
