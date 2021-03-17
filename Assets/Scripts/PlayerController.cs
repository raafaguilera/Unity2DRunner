using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public float JumpForce = 1;
    private float Direction;
    private Rigidbody2D Rigidbody2D;

    private bool CanJump;
    private bool CanMove;

    private bool GameStarted;
    private bool Alive;

    private Animator Animator;

    private BoxCollider2D BoxCollider2D;

    public GameObject BulletPerefab;


    private AudioSource AudioSource;

    public AudioClip CoinSound;
    public AudioClip DeadSound;
    public AudioClip ShootSound;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();

        AudioSource = GetComponent<AudioSource>();

        GameStarted = false;
        CanJump = false;
        Alive = true;
        Direction = 1;
    }

    // Update is called once per frame
    void Update()
    {


        if (GameStarted) {
            if (Alive) {
                if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Direction = 1;
            }

            else if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Direction = -1;

            }


            if (Direction > 0)
            {

                if (Physics2D.Raycast(transform.position + new Vector3(-(BoxCollider2D.size.x / 2 + 0.1f), 0, 0), Vector3.down, 0.15f))
                {
                    Debug.DrawRay(transform.position + new Vector3(-(BoxCollider2D.size.x / 2 + 0.1f), 0, 0), Vector3.down * 0.15f, Color.red);
                    CanJump = true;


                }
                else CanJump = false;

                if (!(Physics2D.Raycast(transform.position + new Vector3(0, (BoxCollider2D.size.y / 2), 0), Vector3.right, (BoxCollider2D.size.x / 2 + 0.018f)) ||
                    Physics2D.Raycast(transform.position + new Vector3(0, -(BoxCollider2D.size.y / 2), 0), Vector3.right, (BoxCollider2D.size.x / 2 + 0.018f))))
                {
                    Debug.DrawRay(transform.position + new Vector3(0, (BoxCollider2D.size.y / 2), 0), Vector3.right * (BoxCollider2D.size.x / 2 + 0.018f), Color.red);
                    Debug.DrawRay(transform.position + new Vector3(0, -(BoxCollider2D.size.y / 2), 0), Vector3.right * (BoxCollider2D.size.x / 2 + 0.018f), Color.red);
                    CanMove = true;
                    Animator.SetBool("running", true);
                    Move();
                }
                else
                {
                    CanMove = false;
                    Animator.SetBool("running", false);
                }


            }
            else if (Direction < 0)
            {



                if (Physics2D.Raycast(transform.position + new Vector3((BoxCollider2D.size.x / 2 + 0.1f), 0, 0), Vector3.down, 0.15f))
                {
                    Debug.DrawRay(transform.position + new Vector3((BoxCollider2D.size.x / 2 + 0.1f), 0, 0), Vector3.down * 0.15f, Color.red);
                    CanJump = true;

                }

                else CanJump = false;

                if (!(Physics2D.Raycast(transform.position + new Vector3(0, (BoxCollider2D.size.y / 2), 0), Vector3.left, (BoxCollider2D.size.x / 2 + 0.018f)) ||
                Physics2D.Raycast(transform.position + new Vector3(0, -(BoxCollider2D.size.y / 2), 0), Vector3.left, (BoxCollider2D.size.x / 2 + 0.018f))))
                {
                    Debug.DrawRay(transform.position + new Vector3(0, (BoxCollider2D.size.y / 2), 0), Vector3.left * (BoxCollider2D.size.x / 2 + 0.018f), Color.red);
                    Debug.DrawRay(transform.position + new Vector3(0, -(BoxCollider2D.size.y / 2), 0), Vector3.left * (BoxCollider2D.size.x / 2 + 0.018f), Color.red);
                    CanMove = true;
                    Animator.SetBool("running", true);
                    Move();
                }
                else
                {
                    CanMove = false;
                    Animator.SetBool("running", false);

                }

            }


            if (Input.GetKeyDown("left ctrl"))
            {
                Shoot();

            }
        }

        }




        if (Input.GetKeyDown(KeyCode.Space) && !GameStarted)
        {
            GameStarted = true;
            
            Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        



        if (Input.GetKeyDown(KeyCode.Space) && CanJump && CanMove) {
            Jump();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("enemy")) {
            Dead();
        }

        if (collision.gameObject.CompareTag("death_zone"))
        {
            Dead();
        }



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gold_coin"))
        {
            AudioSource.PlayOneShot(CoinSound, 1);
            FindObjectOfType<GameManeger>().AddCoinScore(1);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("red_coin"))
        {
            AudioSource.PlayOneShot(CoinSound, 1);
            FindObjectOfType<GameManeger>().AddCoinScore(5);
            Destroy(collision.gameObject);
        }
    }



    private void Jump()
    {

        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Move()
    {
        if (CanMove)
        {
            Rigidbody2D.velocity = new Vector2(Speed*Direction, Rigidbody2D.velocity.y);
            
        }

        
    }

    private void Shoot() {
        GameObject bullet;

        AudioSource.PlayOneShot(ShootSound, 1);

        if (Direction > 0)
        {
            bullet = Instantiate(BulletPerefab, transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
        }
        else
        {
            bullet = Instantiate(BulletPerefab, transform.position + new Vector3(-0.1f, 0, 0), Quaternion.identity);
        }

        bullet.GetComponent<BulletController>().SetDirection(Direction);
    }

    private void Dead() {
        AudioSource.PlayOneShot(DeadSound, 1);
        Alive = false;
        Animator.SetBool("Dead", true);
        Rigidbody2D.bodyType = RigidbodyType2D.Static;
        BoxCollider2D.enabled = false;
        

    
    
    }

    public void DestroyPlayer() {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
