using UnityEngine;
namespace DefaultNamespace
{
    public class GameManager:MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int score = 0;
        public bool isGameOver = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void AddScore(int score){
            this.score += score;
        }

        public void GameOver()
        {
            isGameOver = true;
            // Instantiate GameOver Prefab, dung game 
        }
        
    }
}