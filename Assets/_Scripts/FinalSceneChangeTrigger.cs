using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneChangeTrigger : MonoBehaviour
{
    public string nextSceneName;  // Ge�ilecek sahne ad�

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.allNotesCollected)
            {
                Debug.Log("Kazand�n! T�m notlar topland�.");
                // �stersen kazand�n paneli a� veya direkt sahne y�kle
                GameManager.Instance.LoadFinalScene();
            }
            else
            {
                Debug.Log("Kaybettin! T�m notlar� toplayamad�n.");
                // �stersen kaybettin paneli a� veya ba�ka sahneye y�kle
                //GameManager.Instance.LoadScene("GameOverScene"); // �rnek
                GameManager.Instance.LoadLoseScene();
            }
        }
    }
}

