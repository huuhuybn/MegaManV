using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ssssHPBarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    
    
    private void UpdateHP(int hp)
    {
        // cap nhat giao dien cho hp tai day 
        if (slider.value > 17)
        {
            slider.value = 17;
        }else if (slider.value < 0)
        {
            slider.value = 0;
        }
        else
        {
            slider.value += hp;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
