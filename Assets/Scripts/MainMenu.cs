using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   bool isShowStory = false;
    public GameObject story;
    
    public void Play()
    {
        Debug.Log("Play");
        // truyen vao ten cua Scene
        SceneManager.LoadScene("SampleScene");
    }
    public void Story()
    {   
        // hien thi story
        Debug.Log("Story");
        if (story.activeSelf){
           story.SetActive(false);
        }else{
            story.SetActive(true);
        }
        
    }
    public void Setting()
    {
        // chuyeen sang scene Setting
        Debug.Log("Setting");
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
