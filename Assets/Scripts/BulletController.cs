using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;
    private float Direccion;
    private Rigidbody2D Rigidbody2D;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = new Vector2(Direccion*Speed, Rigidbody2D.velocity.y); 
        
    }

    public void SetDirection(float direction) {
        Direccion = direction;
    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}
