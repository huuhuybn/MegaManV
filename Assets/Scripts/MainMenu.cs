using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   bool isShowStory = false;
    public GameObject story;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.Play();
        Debug.Log("Play");
        // truyen vao ten cua Scene
        SceneManager.LoadScene("SampleScene");
    }
    public void Story()
    {   
        audioSource.Play();
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
        audioSource.Play();
        // chuyeen sang scene Setting
        Debug.Log("Setting");
    }
    public void Exit()
    {
        audioSource.Play();
        Debug.Log("Exit");
        Application.Quit();
    }
}
