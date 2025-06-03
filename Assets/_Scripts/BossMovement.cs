using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer spriteRenderer;

    private Vector2 lastMoveDirection;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        lastMoveDirection = direction.normalized;
        UpdateSpriteDirection();
        // Hareketi baþka yerden yönetiyorsan Rigidbody hareketi ya da transform.position ekle
    }

    void UpdateSpriteDirection()
    {
        if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
        {
            if (lastMoveDirection.x > 0)
                spriteRenderer.sprite = rightSprite;
            else
                spriteRenderer.sprite = leftSprite;
        }
        else
        {
            if (lastMoveDirection.y > 0)
                spriteRenderer.sprite = upSprite;
            else
                spriteRenderer.sprite = downSprite;
        }
    }
}
