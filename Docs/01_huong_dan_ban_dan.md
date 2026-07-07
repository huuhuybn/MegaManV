# Hướng dẫn từng bước (Step-by-Step): Triển khai cơ chế bắn đạn

Để nhân vật của anh có thể bắn đạn bay ra ngoài màn hình thay vì chỉ đứng múa animation, anh hãy làm lần lượt theo các bước sau trong Unity.

---

## Bước 1: Viết Script cho viên đạn (`Bullet.cs`)

Chúng ta cần một script gắn vào viên đạn để điều khiển nó bay đi và tự động biến mất khi chạm tường/quái.

1. Trong cửa sổ Project, vào thư mục `Assets/Scripts/`.
2. Chuột phải -> **Create** -> **C# Script**, đặt tên là `Bullet`.
3. Mở script lên và copy đoạn code sau vào:

```csharp
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;       // Tốc độ bay của đạn
    public float lifetime = 3f;     // Thời gian tự hủy nếu không trúng gì (giảm lag)
    
    private Rigidbody2D rb;
    private int direction = 1;      // 1 là bắn sang phải, -1 là sang trái

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Đạn tự động hủy sau 'lifetime' giây nếu bay ra khỏi map
        Destroy(gameObject, lifetime);
    }

    // Hàm này sẽ được gọi từ PlayerController để truyền hướng bay
    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void Update()
    {
        // Di chuyển viên đạn
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    // Hàm bắt sự kiện khi đạn chạm vào vật thể khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu chạm vào quái (cần gán tag Enemy cho quái)
        if (collision.CompareTag("Enemy"))
        {
            // TODO: Gây sát thương cho quái tại đây (nếu có script Enemy)
            
            // Xóa viên đạn
            Destroy(gameObject);
        }
        // Nếu chạm vào Tường / Mặt đất
        else if (collision.CompareTag("Ground"))
        {
            // Phệu ứng nổ (nếu có)
            Destroy(gameObject);
        }
    }
}
```

---

## Bước 2: Tạo Prefab viên đạn (Bullet) trong Unity

1. Kéo một ảnh Sprite viên đạn (trong thư mục `TaiNguyen` hoặc `Sprites`) ra màn hình **Scene**. Đổi tên nó thành **"Bullet"**.
2. Chọn "Bullet" ở Hierarchy, nhìn sang thẻ **Inspector**:
   - Thêm component **Rigidbody2D**. Ở phần *Body Type*, chọn **Kinematic** (để đạn không bị rớt xuống do trọng lực).
   - Thêm component **BoxCollider2D** (hoặc CircleCollider2D). Nhớ tick vào ô **Is Trigger** (quan trọng).
   - Kéo file script `Bullet.cs` (vừa tạo ở Bước 1) bỏ vào đây.
3. Kéo "Bullet" từ cửa sổ Hierarchy thả vào thư mục `Assets/Prefabs/` để tạo thành một **Prefab**.
4. Xóa cái "Bullet" ngoài màn hình Scene đi.

---

## Bước 3: Tạo vị trí nòng súng (Fire Point)

Nếu viên đạn sinh ra ngay giữa bụng nhân vật thì trông rất vô lý. Chúng ta cần định vị nòng súng.

1. Trong cửa sổ Hierarchy, bấm chuột phải vào nhân vật chính (Player) -> Chọn **Create Empty**.
2. Đổi tên nó thành **"FirePoint"**.
3. Chọn công cụ Move Tool (phím `W`) và kéo "FirePoint" ra ngay vị trí nòng súng/tay của nhân vật.

---

## Bước 4: Cập nhật lại `PlayerController.cs`

Bây giờ chúng ta sẽ sửa lại script của người chơi để nó biết cách sinh ra viên đạn. Anh mở `PlayerController.cs` và cập nhật lại như sau:

```csharp
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab; // Kéo Bullet Prefab vào đây
    public Transform firePoint;     // Kéo FirePoint vào đây

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

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);

        // Lật nhân vật
        if (horizontal > 0)
            spriteRenderer.flipX = true;
        else if (horizontal < 0)
            spriteRenderer.flipX = false;

        if (isShooting) return;

        if (horizontal != 0)
            animator.SetInteger("status", 1);
        else
            animator.SetInteger("status", 0);
    }

    void Shoot()
    {
        // Vừa bấm phím Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            animator.SetInteger("status", 2);
            
            // Gọi hàm sinh ra đạn
            FireBullet();
        }

        // Nhả phím Space
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
            if (horizontal != 0)
                animator.SetInteger("status", 1);
            else
                animator.SetInteger("status", 0);
        }
    }

    // Hàm sinh đạn thực tế
    void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Tạo ra viên đạn tại vị trí nòng súng
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Lấy script Bullet để gán hướng bắn
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Nếu flipX = true (quay sang trái) -> hướng là -1
            // Nếu flipX = false (quay sang phải) -> hướng là 1
            int dir = spriteRenderer.flipX ? -1 : 1;
            bulletScript.SetDirection(dir);
            
            // Lật ảnh viên đạn nếu bắn sang trái (Tùy chọn)
            SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
            if (bulletSprite != null)
            {
                bulletSprite.flipX = spriteRenderer.flipX;
            }
        }
    }
}
```

---

## Bước 5: Gắn tham số trong Unity Editor

1. Quay lại Unity, bấm chọn nhân vật Player.
2. Nhìn sang Inspector, ở script `PlayerController`, anh sẽ thấy xuất hiện 2 ô trống mới:
   - **Bullet Prefab**: Anh kéo file Prefab viên đạn ở trong thư mục `Assets/Prefabs/` bỏ vào ô này.
   - **Fire Point**: Anh kéo cái object **FirePoint** (con của Player) ở cửa sổ Hierarchy bỏ vào ô này.

> [!TIP]
> **Hoàn tất:** Bây giờ anh có thể bấm Play để chạy thử. Bấm Space đạn sẽ bay ra từ tay nhân vật đúng theo hướng nhân vật đang đứng!
