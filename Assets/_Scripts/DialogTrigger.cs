using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [TextArea]
    public List<string> dialogSentences;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            DialogManager.Instance.StartDialog(dialogSentences);
        }
    }
}
