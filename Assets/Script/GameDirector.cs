using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in bu namespace'i ekleyin

public class GameDirector : MonoBehaviour
{
    private int fallenPinsCount = 0;

    public void PinFallen()
    {
        fallenPinsCount++;
        Debug.Log("D��en labut say�s�: " + fallenPinsCount);
    }

    public int GetScore()
    {
        return fallenPinsCount;
    }

    // Oyunu yeniden ba�latma i�levi
    public void RestartGame()
    {
        // Aktif sahneyi yeniden y�kleyerek oyunu yeniden ba�lat�r
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}