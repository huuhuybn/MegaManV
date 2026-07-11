using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    private Vector3 startPosition;
    private int direction = 1;


    void Start()
    {
        startPosition = transform.position;

        // Nếu chưa kéo SpriteRenderer trong Inspector
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Move();
    }


    void Move()
    {
        // Di chuyển
        transform.Translate(
            Vector2.right * direction * moveSpeed * Time.deltaTime
        );
        
        // Flip theo hướng đi
        if (direction > 0)
        {
            spriteRenderer.flipX = true; // Nhìn sang phải
        }
        else
        {
            spriteRenderer.flipX = false;  // Nhìn sang trái
        }
        
        // Đổi hướng khi đi hết khoảng cách

        if (transform.position.x >= startPosition.x + moveDistance)
        {
            direction = -1;
        }
        
        if (transform.position.x <= startPosition.x - moveDistance)
        {
            direction = 1;
        }
    }
}