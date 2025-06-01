using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCinematic_Win : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform boss;
    public Transform playerTargetPosition;
    public Transform bossExitPosition;

    public PlayerMovement playerMovementScript;
    public Animator bossAnimator;

    [Header("Timings")]
    public float playerMoveSpeed = 1f;
    public float bossMoveSpeed = 1f;
    public float delayBeforeBossTurn = 1f;
    public float delayAfterBossExit = 2f;

    [Header("Dialogues")]
    public List<string> initialDialogues;
    public List<string> midDialogues;
    public List<string> endDialogues;

    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(CinematicSequence());
        }
    }

    IEnumerator CinematicSequence()
    {
        // Disable player input
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Move player to target position
        yield return StartCoroutine(MoveToPosition(player, playerTargetPosition.position, playerMoveSpeed));

        // Wait, then boss turns around
        yield return new WaitForSeconds(delayBeforeBossTurn);
        boss.localScale = new Vector3(-boss.localScale.x, boss.localScale.y, boss.localScale.z); // Flip

        // Start initial dialogues
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(initialDialogues));

        // Boss starts pacing (left-right walk)
        StartCoroutine(BossPacing());

        // Mid dialog while boss is pacing
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(midDialogues));

        // Boss walks to exit
        StopCoroutine(BossPacing()); // Optional if you only want one pacing set
        yield return StartCoroutine(MoveToPosition(boss, bossExitPosition.position, bossMoveSpeed));

        // Wait, final dialogues
        yield return new WaitForSeconds(delayAfterBossExit);
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(endDialogues));

        // Scene transition
        SceneManager.LoadScene("GoodEndScene");
    }

    IEnumerator MoveToPosition(Transform obj, Vector3 target, float speed)
    {
        while (Vector3.Distance(obj.position, target) > 0.05f)
        {
            obj.position = Vector3.MoveTowards(obj.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator BossPacing()
    {
        Vector3 left = boss.position + Vector3.left * 0.5f;
        Vector3 right = boss.position + Vector3.right * 0.5f;
        bool goingRight = true;

        while (true)
        {
            Vector3 target = goingRight ? right : left;
            while (Vector3.Distance(boss.position, target) > 0.05f)
            {
                boss.position = Vector3.MoveTowards(boss.position, target, bossMoveSpeed * Time.deltaTime);
                yield return null;
            }
            goingRight = !goingRight;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
