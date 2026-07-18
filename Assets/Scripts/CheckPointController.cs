using System;
using DefaultNamespace;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            // xem co cham vao that ko 
            Debug.Log(other.gameObject.name);
            GameManager.Instance.SaveCheckPoint(
                "level02",1,100,200);
            // chuyen sang scene tiep theo
            
        }
    }
}
