using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float airControlForce = 5.0f;
    public float airControlMax = 5f;
    public bool grounded;
    public AudioSource coinSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()

    {


        if (rigidBody.linearVelocity.x * transform.localScale.x < 0.0f)
            transform.localScale = new Vector3(-transform.localScale.x,
            transform.localScale.y, transform.localScale.z);
        float xSpeed = Mathf.Abs(rigidBody.linearVelocity.x);
        animator.SetFloat("xspeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.linearVelocity.y);
        animator.SetFloat("yspeed", ySpeed);

        float blinkVal = Random.Range(0.0f, 200.0f);
        if (blinkVal < 1.0f)
            animator.SetTrigger("blinktrigger");
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            coinSound.Play();
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        // check if we are on the ground
        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
                rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            else
            {
                // allow a small amount of movement in the air
                float vx = rigidBody.linearVelocityX;
                if (h * vx < airControlMax)
                    rigidBody.AddForce(new Vector2(h * airControlForce, 0));
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
        }
    }


}
 
    


