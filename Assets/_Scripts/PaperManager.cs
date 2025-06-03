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
        // Yeni sahnedeki referansları tazele
        notePopup = GameObject.Find("PopupPanel");
        noteText = GameObject.Find("PopupNoteText")?.GetComponent<TextMeshProUGUI>();
        noteCountText = GameObject.Find("NotesCollectedText")?.GetComponent<TextMeshProUGUI>();

        // PaperNote'ları taramaya devam
        allPapersInScene.Clear();
        var allPapers = FindObjectsOfType<PaperNote>();

        foreach (var paper in allPapers)
        {
            if (collectedNoteIDs.Contains(paper.noteID))
            {
                paper.gameObject.SetActive(false); // Zaten toplandıysa gizle
            }
            else
            {
                allPapersInScene.Add(paper);
            }
        }

        UpdateNoteCount();
    }



    private void FindUIReferences()
    {
        if (notePopup == null)
            notePopup = GameObject.Find("NotePopup");

        if (noteText == null)
        {
            var textObj = GameObject.Find("NoteText");
            if (textObj != null) noteText = textObj.GetComponent<TextMeshProUGUI>();
        }

        if (noteCountText == null)
        {
            var countObj = GameObject.Find("NoteCountText");
            if (countObj != null) noteCountText = countObj.GetComponent<TextMeshProUGUI>();
        }

        if (notePopup == null || noteText == null || noteCountText == null)
        {
            Debug.LogWarning("PaperManager: UI referansları eksik, sahnede doğru isimleri verdiğinden emin ol.");
        }
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
        if (noteText != null) noteText.text = note.noteContent;
        if (notePopup != null) notePopup.SetActive(true);
    }

    public void CloseNote()
    {
        if (notePopup != null) notePopup.SetActive(false);

        if (lastShownNote != null && lastShownNote.dialogLines != null && lastShownNote.dialogLines.Count > 0)
        {
            DialogManager.Instance.StartDialog(lastShownNote.dialogLines);
        }
    }

    public bool Collected(string noteID)
    {
        return collectedNoteIDs.Contains(noteID);
    }

    void UpdateNoteCount()
    {
        if (noteCountText != null)
            noteCountText.text = $"Notes Collected: {collectedNoteIDs.Count} / {totalNotes}";
    }
}
