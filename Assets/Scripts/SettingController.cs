using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
  
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider volumeSlider;
    public Slider fbxSlider;
    public Toggle muteToggle;
    void Start()
    {
        // khi mở màn hình thì lấy dữ liệu cũ , cập nhập vào slider
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        fbxSlider.value = PlayerPrefs.GetFloat("fbx");
        muteToggle.isOn = PlayerPrefs.GetInt("mute") == 1 ? true : false;
    }
    public void Save()
    {
        float volume = volumeSlider.value;
        float fbx = fbxSlider.value;
        bool mute = muteToggle.isOn ? true : false;
        // Class PlayerPrefs cho phép lưu biến vào bộ nhớ , gọi ra ở các lần sau 
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("fbx", fbx);
        PlayerPrefs.SetInt("mute", mute ? 1 : 0);
        PlayerPrefs.Save();
        // Hiển thị thông báo đã lưu thành công 
    }
    public void Back()
    { // Quay tro lai MenuScene
        SceneManager.LoadScene("MenuScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
