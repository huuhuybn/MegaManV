using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // bat da tu vi tri dau tien
        scrollRect.verticalNormalizedPosition = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        scrollRect.verticalNormalizedPosition = scrollSpeed * Time.deltaTime;
        // neu cuon het thi dung lai
        if (scrollRect.verticalNormalizedPosition <= 0){
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}
