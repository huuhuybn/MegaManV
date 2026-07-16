using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string playerName;
    [TextArea(3, 10)]
    public string description;
    
    public Sprite player;
    
    public AnimatorController animatorController;
    
    public int hp;
    public int maxHp;

    public int damage;
    
    public float speed;

}
