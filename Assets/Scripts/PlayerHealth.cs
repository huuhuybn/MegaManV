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
        }
        
        
    }
}