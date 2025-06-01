using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SplitToningLerp : MonoBehaviour
{
    [Header("Volume & Profile")]
    public Volume volume; // Sahnedeki Volume component
    private SplitToning splitToning;

    [Header("Split Toning Colors")]
    public Color shadowColorA = Color.blue;
    public Color shadowColorB = Color.green;

    public Color highlightColorA = Color.red;
    public Color highlightColorB = Color.yellow;

    [Header("Blend Settings")]
    [Range(0f, 1f)] public float blendSpeed = 0.5f; // Ge�i� h�z�
    private float blendValue = 0f;
    private bool increasing = true;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume component atanmad�!");
            enabled = false;
            return;
        }

        if (!volume.profile.TryGet(out splitToning))
        {
            Debug.LogError("SplitToning component bulunamad�!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Blend de�eri 0 ile 1 aras�nda gidip gelsin
        if (increasing)
        {
            blendValue += Time.deltaTime * blendSpeed;
            if (blendValue >= 1f)
            {
                blendValue = 1f;
                increasing = false;
            }
        }
        else
        {
            blendValue -= Time.deltaTime * blendSpeed;
            if (blendValue <= 0f)
            {
                blendValue = 0f;
                increasing = true;
            }
        }

        // Renkleri lineer kar��t�r (Lerp)
        Color currentShadow = Color.Lerp(shadowColorA, shadowColorB, blendValue);
        Color currentHighlight = Color.Lerp(highlightColorA, highlightColorB, blendValue);

        // SplitToning renklerini uygula
        splitToning.shadows.value = currentShadow;
        splitToning.highlights.value = currentHighlight;
    }
}
