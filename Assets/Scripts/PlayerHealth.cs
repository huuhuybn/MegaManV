using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerHealth: MonoBehaviour

    {
        // array thanh mau 
        public Sprite[] healthSprites;
        private SpriteRenderer spriteRenderer;
        // dang ki su kien action tang giam mau tu GameManager 

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = healthSprites[0]; // gan anh full hp 
        }

        private void UpdateHP(int hp)
        {
            // cap nhat giao dien cho hp tai day 
        }


    }
}