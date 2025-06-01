using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    public string nextSceneName;  // Geçilecek sahne adý

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadKMFScene();
        }
    }
}
