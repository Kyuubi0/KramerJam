using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance;

    public GameObject notePopup;
    public TextMeshProUGUI noteText;      // TextMeshPro tipinde olmal�!
    public TextMeshProUGUI noteCountText;

    private List<PaperNote> collectedNotes = new List<PaperNote>();
    private int totalNotes = 17; // Ka��t say�s�n� buraya yazabilirsin, de�i�tirilebilir

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowNote(PaperNote note)
    {
        if (!collectedNotes.Contains(note))
        {
            collectedNotes.Add(note);
            UpdateNoteCount();
        }

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