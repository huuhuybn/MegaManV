using System;
using UnityEngine;
namespace DefaultNamespace
{
    public class GameManager:MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int score = 0;
        public int health = 17;
        // khai bao Action khi hp thay doi 
        // khai bao Action khi diem thay doi  
        public Action<int> OnScoreChange;
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
            if(isGameOver) return;
            this.score += score;
            // thong bao diem thay doi 
            OnScoreChange?.Invoke(this.score);
        }

        public void ChangeHP(int hp)
        {
            // hp la = -1 hoac 1 .
            // kiem tra max = 17 , min = 0 
            // < 0 thi la gameOver 
        }
        public void GameOver()
        {
            isGameOver = true;
            // Instantiate GameOver Prefab, dung game 
        }
    }
}