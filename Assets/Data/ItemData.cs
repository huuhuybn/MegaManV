using UnityEngine;

/// <summary>
/// fileName : ten cua file nay
/// menuName : ten cua menu 
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/SubItem")]
public class ItemData : ScriptableObject
{
    // Định nghĩa ra các mục để điền thông tin 
    [Header("Thông tin cơ bản")]
    public string itemName;
    
    [TextArea(3, 10)]
    public string itemDescription;
    public Sprite itemSprite; // ảnh item
    
    [Min(0)]
    public int price;

    [Min(0)] public int damage;
    
    // tạo ra 1 bộ khung dữ liệu để tái sử dụng nhiều lần trong tương lai 
    // vật phẩm : Súng Lục, AK , Kiếm, Dao, Áo, Quần  ...

}
