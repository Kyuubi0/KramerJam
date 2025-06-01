using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    public string nextSceneName;  // Ge�ilecek sahne ad�

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadKMFScene();
        }
    }
}
