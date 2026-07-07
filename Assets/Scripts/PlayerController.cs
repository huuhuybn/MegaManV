using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    public GameObject bulletPrefab;
    // gan firePoint = Right ? Left 
    private Transform firePoint;
    
    [Header("References")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float horizontal;
    private bool isShooting;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Move();
        Shoot();
    }
    // ==========================
    // DI CHUYỂN
    // ==========================
    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(
            Vector2.right * horizontal * moveSpeed * Time.deltaTime
        );
        // Lật nhân vật
        if (horizontal > 0)
            spriteRenderer.flipX = true;
        else if (horizontal < 0)
            spriteRenderer.flipX = false;
        // Nếu đang bắn thì giữ animation Shoot
        if (isShooting)
            return;
        
        // Idle / Run
        if (horizontal != 0)
            animator.SetInteger("status", 1); // Run
        else
            animator.SetInteger("status", 0); // Idle
    }


    // ==========================
    // BẮN
    // ==========================
    void Shoot()
    {
        // Nhấn Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            animator.SetInteger("status", 2); // Shoot
            // 1. Instantiate Bullet Prefab tai vi tri firepoint 
            Bullet bullet = bulletPrefab.gameObject.GetComponent<Bullet>();
           if (horizontal > 0)
           {
               bullet.setDirection(1);
               firePoint = gameObject.transform.GetChild(1);
           }
            else
            {
                bullet.setDirection(-1);
                firePoint = gameObject.transform.GetChild(2);
            }
            // xuat hien vien dan o vi tri firepoint 
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        // Nhả Space
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
            if (horizontal != 0)
                animator.SetInteger("status", 1); // Run
            else
                animator.SetInteger("status", 0); // Idle
        }
    }
}