using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string characterName;
    
    [TextArea(3, 6)]
    public string description;
    
    [Header("Hình ảnh")]
    public Sprite avatar;
    public Sprite characterSprite;
    public GameObject characterPrefab;

    [Header("Chỉ số")]
    [Min(1)]
    public int maxHealth = 100;

    [Min(0)]
    public int attackDamage = 20;

    [Min(0)]
    public int defense = 5;

    [Min(0)]
    public float moveSpeed = 5f;

    [Min(0)]
    public float jumpForce = 10f;

    [Header("Thông tin nâng cao")]
    [Min(1)]
    public int level = 1;

    [Min(0)]
    public int experience;

    [Min(0)]
    public int price;

    public bool isUnlocked = true;
}
