using DefaultNamespace;
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
    
    public CharacterData[] characterData;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = characterData[0].player;
        animator.runtimeAnimatorController = characterData[0].animatorController;
        // truy cap thong tin 
        GameManager.Instance.MaxHP = characterData[0].hp;
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            isShooting = true;
            animator.SetInteger("status", 2);
            FireLaser();
        }
        
        // Nhả Space or L 
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.L))
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

    public void FireLaser()
    {
        // 1 lay ra gameobject con chua LineRender 
        bool isFacingRight = spriteRenderer.flipX;
        firePoint = gameObject.transform.GetChild(isFacingRight ? 2 : 1);
        float distance = isFacingRight ? 5 : -5;
        Vector2 targetPoint = new Vector2(firePoint.position.x + distance, firePoint.position.y);
        LineRenderer laserRend = gameObject.transform.GetChild(4).gameObject.
            GetComponent<LineRenderer>();
        // thiet lap 2 point ( size = 2 ) , firepoint va targetpoint 
        laserRend.positionCount = 2; 
        laserRend.SetPosition(0, firePoint.position);
        laserRend.SetPosition(1, targetPoint);
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        // tich hop kiem tra va cham 
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction,5);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag.Equals("Enemy"))
            {
                // 
            }
        }
        // Raycast : kiem tra va cham tren 1 duong thang 
    }
}