using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance;

    public GameObject notePopup;
    public TextMeshProUGUI noteText;
    public TextMeshProUGUI noteCountText;

    private HashSet<string> collectedNoteIDs = new HashSet<string>();
    private List<PaperNote> allPapersInScene = new List<PaperNote>();

    private PaperNote lastShownNote;
    [SerializeField] private int totalNotes = 17;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Yeni sahnedeki tüm PaperNote'larý bul
        allPapersInScene.Clear();
        var allPapers = FindObjectsOfType<PaperNote>();

        foreach (var paper in allPapers)
        {
            if (collectedNoteIDs.Contains(paper.noteID))
            {
                paper.gameObject.SetActive(false); // Zaten toplandýysa gizle
            }
            else
            {
                allPapersInScene.Add(paper); // Scene'deki aktif paper'larý tut
            }
        }

        UpdateNoteCount(); // UI'ý güncelle
    }

    public void CollectNote(PaperNote note)
    {
        if (!collectedNoteIDs.Contains(note.noteID))
        {
            collectedNoteIDs.Add(note.noteID);
            UpdateNoteCount();

            if (collectedNoteIDs.Count >= totalNotes)
            {
                GameManager.Instance.allNotesCollected = true;
            }
        }
    }

    public void ShowNote(PaperNote note)
    {
        lastShownNote = note;
        noteText.text = note.noteContent;
        notePopup.SetActive(true);
    }

    public void CloseNote()
    {
        notePopup.SetActive(false);

        if (lastShownNote != null && lastShownNote.dialogLines != null && lastShownNote.dialogLines.Count > 0)
        {
            DialogManager.Instance.StartDialog(lastShownNote.dialogLines);
        }
    }

    void UpdateNoteCount()
    {
        noteCountText.text = $"Notes Collected: {collectedNoteIDs.Count} / {totalNotes}";
    }
}