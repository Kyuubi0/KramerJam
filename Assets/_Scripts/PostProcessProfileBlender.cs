using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SplitToningLoopSmooth : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile profileA;
    public VolumeProfile profileB;
    public float blendSpeed = 1f;

    private SplitToning splitToningA;
    private SplitToning splitToningB;
    private SplitToning activeSplitToning;

    void Start()
    {
        if (volume == null || profileA == null || profileB == null)
        {
            Debug.LogError("Volume ve profilleri atamayý unutma!");
            enabled = false;
            return;
        }

        // Volume profili kopyala, deðiþiklik için
        volume.profile = Instantiate(profileA);

        if (!profileA.TryGet(out splitToningA) ||
            !profileB.TryGet(out splitToningB) ||
            !volume.profile.TryGet(out activeSplitToning))
        {
            Debug.LogError("SplitToning componentleri bulunamadý!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // PingPong ile 0 ile 1 arasý sürekli gidip geliyor
        float t = Mathf.PingPong(Time.time * blendSpeed, 1f);

        activeSplitToning.shadows.value = Color.Lerp(splitToningA.shadows.value, splitToningB.shadows.value, t);
        activeSplitToning.highlights.value = Color.Lerp(splitToningA.highlights.value, splitToningB.highlights.value, t);
        activeSplitToning.balance.value = Mathf.Lerp(splitToningA.balance.value, splitToningB.balance.value, t);
    }
}
