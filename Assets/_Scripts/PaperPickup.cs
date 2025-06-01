using UnityEngine;

public class PaperPickup : MonoBehaviour
{
    private PaperNote paperNote;
    private bool isCollected = false;

    void Awake()
    {
        paperNote = GetComponent<PaperNote>();
        if (paperNote == null)
        {
            Debug.LogError("PaperNote component is missing on " + gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            if (paperNote != null)
            {
                isCollected = true;
                PaperManager.Instance.CollectNote(paperNote);
                PaperManager.Instance.ShowNote(paperNote);
                gameObject.SetActive(false);
            }
        }
    }
}