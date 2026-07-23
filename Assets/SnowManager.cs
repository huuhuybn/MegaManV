using UnityEngine;

public class SnowManager : MonoBehaviour
{
     private ParticleSystem snowParticleSystem;
   
       void Start()
       {
           // 1. Tự động thêm thành phần Particle System vào GameObject nếu chưa có
           snowParticleSystem = gameObject.GetComponent<ParticleSystem>();
           if (snowParticleSystem == null)
           {
               snowParticleSystem = gameObject.AddComponent<ParticleSystem>();
           }
   
           SetupSnowEffect();
       }
   
       void SetupSnowEffect()
       {
           // Loại bỏ Material mặc định dạng 3D khối (pink) sang dạng hạt mờ 2D
           ParticleSystemRenderer psRenderer = GetComponent<ParticleSystemRenderer>();
           psRenderer.material = new Material(Shader.Find("Sprites/Default"));
   
           // Thay đổi vị trí hệ thống phát hạt lên phía trên màn hình
           transform.position = new Vector3(0, 6, 0);
   
           // 2. Thiết lập các thông số chính (Main Module)
           var main = snowParticleSystem.main;
           main.startLifetime = 10.0f;          // Thời gian tồn tại của hạt tuyết (giây)
           main.startSpeed = new ParticleSystem.MinMaxCurve(1.5f, 3.0f); // Tốc độ rơi ngẫu nhiên
           main.startSize = new ParticleSystem.MinMaxCurve(0.05f, 0.2f); // Kích thước hạt ngẫu nhiên
           main.gravityModifier = 0.1f;        // Lực hấp dẫn nhẹ để tuyết rơi xuống tự nhiên
           main.loop = true;                   // Lặp lại vô hạn
           main.playOnAwake = true;
   
           // 3. Thiết lập vùng phát hạt (Shape Module) dạng đường thẳng hoặc hộp chữ nhật phẳng
           var shape = snowParticleSystem.shape;
           shape.shapeType = ParticleSystemShapeType.Box;
           shape.scale = new Vector3(15, 0.5f, 0); // Độ rộng vùng rơi (phủ kín chiều ngang màn hình 2D)
   
           // 4. Thiết lập số lượng hạt (Emission Module)
           var emission = snowParticleSystem.emission;
           emission.rateOverTime = 50;         // Số lượng hạt tuyết rơi mỗi giây
   
           
       }
}
