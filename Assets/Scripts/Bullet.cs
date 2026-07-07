using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    // thoi gian ton tai cua vien dien 
    public float lifetime = 3f;
    private int direction = 1; // 1 : right , -1 : left 
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       Destroy(gameObject, lifetime); 
    }
    public void setDirection(int direction)
    {
        if (direction > 0)
        {
         spriteRenderer.flipX = true;   
        }else spriteRenderer.flipX = false;
        
        this.direction = direction;
    }
    // Update is called once per frame
    void Update()
    {
        // di chuyen vien dan 
        transform.Translate(Vector2.right * direction 
                                          * speed * Time.deltaTime);
    }
    // bat su kien va cham 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
          // tru mau cua enemy   
        }else if (collision.tag.Equals("Ground"))
        {
            // bo sung hieu ung vu no tai day !!!
            Destroy(gameObject);
        }
    }
}
