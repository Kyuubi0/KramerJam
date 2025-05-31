using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public float smoothSpeed = 0.125f; // Kameranýn takip yumuþaklýðý

    public List<BoxCollider2D> cameraBounds; // Kamera alaný sýnýrlarýný belirleyen collider listesi

    private float camHalfHeight;
    private float camHalfWidth;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: Target atanmamýþ!");
            enabled = false;
            return;
        }

        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 clampedPosition = ClampPositionToBounds(desiredPosition);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        foreach (var bound in cameraBounds)
        {
            if (bound.OverlapPoint(target.position))
            {
                Bounds b = bound.bounds;

                float clampedX = Mathf.Clamp(position.x, b.min.x + camHalfWidth, b.max.x - camHalfWidth);
                float clampedY = Mathf.Clamp(position.y, b.min.y + camHalfHeight, b.max.y - camHalfHeight);

                return new Vector3(clampedX, clampedY, position.z);
            }
        }

        // Eðer sýnýr içinde deðilse direkt pozisyonu döndür
        return position;
    }
}
