using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCinematic_Win : MonoBehaviour
{
    public Transform player;
    public Transform boss;
    public SpriteRenderer bossRenderer;
    public Sprite bossLeftSprite, bossRightSprite, bossBackSprite, bossFrontSprite;

    public Transform playerTargetPosition;
    public Transform bossExitPosition;
    public float moveSpeed = 2f;
    public float bossPaceDuration = 2f; // Saða-sola yürüme süresi

    public List<string> dialogPhase1;
    public List<string> dialogPhase2;
    public List<string> dialogPhase3;
    public List<string> dialogPhase4;

    public GameObject fadeToBlackPanel; // UI Panel (Canvas > Image) - siyah panel

    [SerializeField] private bool isCinematicRunning = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCinematicRunning && other.CompareTag("Player"))
        {
            isCinematicRunning = true;
            StartCoroutine(CinematicSequence());
        }
    }

    IEnumerator CinematicSequence()
    {
        print("cagýrýldým.");
        // 1. Player hedef pozisyona gider
        yield return MoveToPosition(player, playerTargetPosition.position);

        // Boss bize döner
        bossRenderer.sprite = bossFrontSprite;

        // Diyalog 1
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(dialogPhase1));

        // 2. Boss sað-sol volta
        yield return StartCoroutine(BossPaceRoutine());

        // Diyalog 2
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(dialogPhase2));

        // 3. Boss exit pozisyona yürür
        yield return MoveToPosition(boss, bossExitPosition.position);
        bossRenderer.sprite = bossBackSprite;

        // Diyalog 3
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(dialogPhase3));

        // 4. Son diyalog
        yield return StartCoroutine(DialogManager.Instance.StartDialogRoutine(dialogPhase4));

        // Fade to black
        fadeToBlackPanel.SetActive(true);
        CanvasGroup canvasGroup = fadeToBlackPanel.GetComponent<CanvasGroup>();
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t;
            yield return null;
        }

        // Sahne geçiþi
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GoodEndScene");
    }

    IEnumerator MoveToPosition(Transform obj, Vector3 targetPos)
    {
        while (Vector3.Distance(obj.position, targetPos) > 0.1f)
        {
            obj.position = Vector3.MoveTowards(obj.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    IEnumerator BossPaceRoutine()
    {
        float elapsed = 0f;
        bool goRight = true;

        Vector3 originalPos = boss.position;
        Vector3 offset = new Vector3(1.5f, 0, 0);

        while (elapsed < bossPaceDuration)
        {
            Vector3 target = goRight ? originalPos + offset : originalPos - offset;
            bossRenderer.sprite = goRight ? bossRightSprite : bossLeftSprite;

            while (Vector3.Distance(boss.position, target) > 0.1f)
            {
                boss.position = Vector3.MoveTowards(boss.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }

            goRight = !goRight;
            elapsed += 1f;
        }
    }
}
