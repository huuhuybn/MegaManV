using System;
using DefaultNamespace;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag.Equals("Player"))
        {
            // Gui event tang mau cho nhan vat 
            GameManager.Instance.ChangeHP(GameManager.Instance.MaxHP);
            // Xuat hien hieu ung particle system 
            GameObject par = 
                Instantiate(particlePrefab, transform.position, transform.rotation);
            Destroy(par,2);  // xoa hieu ung sau 2giay 
            Destroy(gameObject); // bien mat vat pham 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
