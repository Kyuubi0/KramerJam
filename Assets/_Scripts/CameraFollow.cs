using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Oyuncu
    public Collider2D[] boundsColliders; // Sýnýr colliderlarý

    private float halfHeight;
    private float halfWidth;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        if (boundsColliders == null || boundsColliders.Length == 0)
        {
            Debug.LogError("Bounds Colliders atanmamýþ!");
            return;
        }

        Bounds combinedBounds = boundsColliders[0].bounds;
        for (int i = 1; i < boundsColliders.Length; i++)
        {
            combinedBounds.Encapsulate(boundsColliders[i].bounds);
        }

        minBounds = combinedBounds.min;
        maxBounds = combinedBounds.max;
    }

    void LateUpdate()
    {
        if (target == null || boundsColliders == null || boundsColliders.Length == 0)
            return;

        float clampedX = Mathf.Clamp(target.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(target.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}