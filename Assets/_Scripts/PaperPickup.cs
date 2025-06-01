using UnityEngine;

public class PaperPickup : MonoBehaviour
{
    private PaperNote paperNote;

    private void Start()
    {
        paperNote = GetComponent<PaperNote>();
        if (paperNote == null)
        {
            Debug.LogError("PaperNote component not found on this object!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && paperNote != null)
        {
            PaperManager.Instance.CollectNote(paperNote);
            gameObject.SetActive(false);
        }
    }
}