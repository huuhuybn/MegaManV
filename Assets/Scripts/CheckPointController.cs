using System;
using DefaultNamespace;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
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
