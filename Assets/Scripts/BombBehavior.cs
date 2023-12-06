using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BombBehavior : MonoBehaviour
{
    float timer = 5f;
    public float speed = 10f;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip activeSound;
    public AudioClip explosionSound; 
    public bool colliding = false;
    public bool dragging = false;
    public bool isDraggable = true;
    public bool explode = false;
   
    void Update()
    {
        if (dragging == true)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
        explodeBomb();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.PlayOneShot(activeSound);
        Launch();
    }

    // Update is called once per frame
   void FixedUpdate()
    {
       
    }

    void Launch()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        rb.velocity = new Vector3(speed*x, speed*y, 0);
    }

    private void explodeBomb()
    {
        if (timer <= 0)
        {
            //explota y envia el mensaje a game manager
            explode = true;
            animator.SetBool("GameOver", true);
            rb.velocity = new Vector3(0, 0, 0);
            bc.enabled = false;
        }
        else if (dragging == false && colliding == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 3)
            {
                animator.SetBool("Danger", true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Red" || collision.gameObject.tag == "Black")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Base"))
        {
            isDraggable = true;
        }
        //Colisiona y se suelta el boton del mouse
        if (collision.gameObject.tag != gameObject.tag && collision.gameObject.tag != "Base" && dragging == false)
        {
            animator.SetBool("GameOver", true);
            explode = true;
        }

        if (collision.gameObject.tag == gameObject.tag && dragging == false)
        {
            colliding = true;
            isDraggable = false;
            animator.SetBool("Secured", true);
            audioSource.mute = true;
        }
    }
}
