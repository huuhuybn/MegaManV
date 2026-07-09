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

    public void Fire()
    {
        // 1. Xác định firePoint dựa vào hướng nhân vật đang quay mặt (flipX)
        bool isFacingRight = spriteRenderer.flipX;
        firePoint = gameObject.transform.GetChild(isFacingRight ? 2 : 1);
        Debug.Log(firePoint.name);
        // 2. Xuất hiện viên đạn ở vị trí firepoint và lấy component Bullet của VIÊN ĐẠN MỚI TẠO
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        // 3. Cài đặt hướng bay
        if (isFacingRight) 
            bullet.setDirection(1);
        else bullet.setDirection(-1);
    }
}