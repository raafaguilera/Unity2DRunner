using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    public float Velocity;

    private bool Alive;

    public AudioClip EnemyDeadSound;

    private float Direction;
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Direction = 1;
        Alive = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Alive)
        {


            if (Direction > 0)
            {
                if (Physics2D.Raycast(transform.position + new Vector3((BoxCollider2D.size.x / 2), 0, 0), Vector3.down, 0.15f))
                {
                    Debug.DrawRay(transform.position + new Vector3((BoxCollider2D.size.x / 2), 0, 0), Vector3.down * 0.15f, Color.red);
                    Rigidbody2D.velocity = new Vector2(Velocity * Direction, Rigidbody2D.velocity.y);
                }
                else
                {
                    Direction = -Direction;
                    transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else if (Direction < 0)
            {
                if (Physics2D.Raycast(transform.position + new Vector3(-(BoxCollider2D.size.x / 2), 0, 0), Vector3.down, 0.15f))
                {
                    Debug.DrawRay(transform.position + new Vector3(-(BoxCollider2D.size.x / 2), 0, 0), Vector3.down * 0.15f, Color.red);
                    Rigidbody2D.velocity = new Vector2(Velocity * Direction, Rigidbody2D.velocity.y);

                }
                else
                {
                    Direction = -Direction;
                    transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

            }
        }

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) {
            FindObjectOfType<GameManeger>().AddEnemyScore();
            Destroy(collision.gameObject);
            GetComponent<Animator>().SetBool("Dead", true);
            Alive = false;
            Rigidbody2D.bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    public void DestroyEnemy() {
        GetComponent<AudioSource>().PlayOneShot(EnemyDeadSound, 1);
        Destroy(gameObject);
    }

}



