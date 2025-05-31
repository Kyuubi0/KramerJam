using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingSequenceController : MonoBehaviour
{
    [Header("Sprite Renderers")]
    public SpriteRenderer Room;
    public SpriteRenderer Light1;
    public SpriteRenderer View;
    public SpriteRenderer Light2;
    public SpriteRenderer Frame;

    [Header("UI")]
    public TextMeshProUGUI DialogText;
    public Button ContinueButton;

    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public int lightPulseCount = 3;

    private void Awake()
    {
        SetAlpha(Room, 0);
        SetAlpha(Light1, 0);
        SetAlpha(View, 0);
        SetAlpha(Light2, 0);
        SetAlpha(Frame, 0);

        DialogText.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        StartCoroutine(FadeIn(Room, fadeDuration));
        yield return StartCoroutine(FadeIn(Light1, fadeDuration));

        yield return StartCoroutine(Pulse(Light1, lightPulseCount, 0.2f, 1f));
        yield return StartCoroutine(FadeOut(Light1, fadeDuration));
        Light1.gameObject.SetActive(false);

        DialogText.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText("Neler oluyor?"));

        ContinueButton.gameObject.SetActive(true);
        bool clicked = false;
        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);

        DialogText.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);

        StartCoroutine(FadeOut(Room, fadeDuration));
        yield return StartCoroutine(FadeOut(Light1, fadeDuration)); // Light1 zaten gizli ama sorun yok

        StartCoroutine(FadeIn(View, fadeDuration));
        StartCoroutine(FadeIn(Frame, fadeDuration));
        yield return StartCoroutine(FadeIn(Light2, fadeDuration, Light2.color.a));

        DialogText.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText("Kuzey ýþýklarý mý, ne alaka?"));

        clicked = false;
        ContinueButton.gameObject.SetActive(true);
        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => clicked = true);
        yield return new WaitUntil(() => clicked);

        DialogText.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);

        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator ShowText(string message, float delay = 0.05f)
    {
        DialogText.text = "";
        foreach (char c in message)
        {
            DialogText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    private void SetAlpha(SpriteRenderer sr, float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    private IEnumerator FadeIn(SpriteRenderer sr, float duration, float? targetAlphaOverride = null)
    {
        float elapsed = 0f;
        Color original = sr.color;
        float targetAlpha = targetAlphaOverride ?? 1f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsed / duration);
            sr.color = new Color(original.r, original.g, original.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(original.r, original.g, original.b, targetAlpha);
    }

    private IEnumerator FadeOut(SpriteRenderer sr, float duration)
    {
        float elapsed = 0f;
        Color original = sr.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(original.a, 0f, elapsed / duration);
            sr.color = new Color(original.r, original.g, original.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(original.r, original.g, original.b, 0f);
    }

    private IEnumerator Pulse(SpriteRenderer sr, int times, float minAlpha, float maxAlpha)
    {
        for (int i = 0; i < times; i++)
        {
            yield return StartCoroutine(FadeAlpha(sr, maxAlpha, minAlpha, 0.3f));
            yield return StartCoroutine(FadeAlpha(sr, minAlpha, maxAlpha, 0.3f));
        }
    }

    private IEnumerator FadeAlpha(SpriteRenderer sr, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = sr.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            sr.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(color.r, color.g, color.b, to);
    }
}
