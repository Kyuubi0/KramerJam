using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance;

    public GameObject notePopup;        // Popup paneli
    public TextMeshProUGUI noteText;    // Not metni
    public TextMeshProUGUI noteCountText; // Toplanan kaðýt sayýsý metni

    private List<PaperNote> collectedNotes = new List<PaperNote>();
    private int totalNotes = 17;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CollectNote(PaperNote note)
    {
        if (!collectedNotes.Contains(note))
        {
            collectedNotes.Add(note);
            UpdateNoteCount();
        }
    }

    public void ShowNote(PaperNote note)
    {
        noteText.text = note.noteContent;
        notePopup.SetActive(true);
    }

    public void CloseNote()
    {
        notePopup.SetActive(false);
    }

    void UpdateNoteCount()
    {
        noteCountText.text = $"Notes Collected: {collectedNotes.Count} / {totalNotes}";
    }
}