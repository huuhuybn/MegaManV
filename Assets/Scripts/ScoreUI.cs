using TMPro;
using UnityEngine;
using DefaultNamespace;

public class ScoreUI : MonoBehaviour
{
    // Kéo thả ScoreText GameObject vào đây trong Inspector
    public TMP_Text scoreText;

    private void Start()
    {
        // Hiển thị điểm ban đầu
        UpdateScore(0);

        // Đăng ký lắng nghe sự kiện OnScoreChange của GameManager
        // Mỗi khi GameManager.AddScore() được gọi → UpdateScore tự chạy
        GameManager.Instance.OnScoreChange += UpdateScore;
    }

    // Hàm này được gọi tự động khi điểm thay đổi
    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private void OnDestroy()
    {
        // Hủy đăng ký khi UI bị xóa (tránh lỗi bộ nhớ khi chuyển scene)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChange -= UpdateScore;
        }
    }
}
