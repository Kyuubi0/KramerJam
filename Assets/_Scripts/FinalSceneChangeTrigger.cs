using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneChangeTrigger : MonoBehaviour
{
    public string nextSceneName;  // Geçilecek sahne adý

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.allNotesCollected)
            {
                Debug.Log("Kazandýn! Tüm notlar toplandý.");
                // Ýstersen kazandýn paneli aç veya direkt sahne yükle
                GameManager.Instance.LoadFinalScene();
            }
            else
            {
                Debug.Log("Kaybettin! Tüm notlarý toplayamadýn.");
                // Ýstersen kaybettin paneli aç veya baþka sahneye yükle
                //GameManager.Instance.LoadScene("GameOverScene"); // Örnek
                GameManager.Instance.LoadLoseScene();
            }
        }
    }
}

