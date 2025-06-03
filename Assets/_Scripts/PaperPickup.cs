using UnityEngine;

public class PaperPickup : MonoBehaviour
{
    private PaperNote paperNote;

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
        if (other.CompareTag("Player"))
        {
            if (paperNote != null && !PaperManager.Instance.Collected(paperNote.noteID))
            {
                PaperManager.Instance.CollectNote(paperNote);
                PaperManager.Instance.ShowNote(paperNote);
                gameObject.SetActive(false);
            }
        }
    }
}
