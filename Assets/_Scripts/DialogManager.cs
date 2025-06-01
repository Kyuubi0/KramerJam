using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button nextButton;

    private Queue<string> sentences = new Queue<string>();
    private bool isDialogActive = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialog(List<string> dialogSentences)
    {
        if (isDialogActive) return;

        isDialogActive = true;
        dialogPanel.SetActive(true);
        sentences.Clear();

        foreach (var sentence in dialogSentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        StopAllCoroutines();
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.03f); // Yazma hýzý
        }
    }

    void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);
    }
}