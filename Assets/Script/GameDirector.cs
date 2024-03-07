using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için bu namespace'i ekleyin

public class GameDirector : MonoBehaviour
{
    private int fallenPinsCount = 0;

    public void PinFallen()
    {
        fallenPinsCount++;
        Debug.Log("Düþen labut sayýsý: " + fallenPinsCount);
    }

    public int GetScore()
    {
        return fallenPinsCount;
    }

    // Oyunu yeniden baþlatma iþlevi
    public void RestartGame()
    {
        // Aktif sahneyi yeniden yükleyerek oyunu yeniden baþlatýr
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}