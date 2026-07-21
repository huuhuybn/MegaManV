using System;
using System.IO;
using UnityEngine;
namespace DefaultNamespace
{
    public class GameManager:MonoBehaviour
    {
        private string[] level = new[] { "level01", "level02", "level03"};
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

        public void SaveCheckPoint(string sceneName,
            int checkPoint, int health, int score)
        {
            SaveData saveData = new SaveData();
            saveData.checkPoint = checkPoint;
            saveData.health = health;
            saveData.score = score;
            saveData.checkPoint = checkPoint;
            string json = JsonUtility.ToJson(saveData);
            string path =  Application.persistentDataPath + "/SaveData.json";
            Debug.Log(path);
            File.WriteAllText(path, json);
            //File.WriteAllText(path, json);
            //File.WriteAllText(path, json);
            Debug.Log("Saved checkpoint.!!!!");
        }

        public bool IsCheckPointExist()
        {
            string path =  Application.persistentDataPath + "/SaveData.json";
            return File.Exists(path);
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