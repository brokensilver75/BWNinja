using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed, jumpForce, jumpTime, jumpVelocity;
    [SerializeField] bool grounded;
    public float moveX, moveY;
    public Animator anim;
    Rigidbody2D playerRb;
    Vector2 direction;
    //public PlayerController playerControllerInstance;
    //[SerializeField] ParticleSystem smoke;
    bool startedRunning = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        anim.SetBool("Grounded", true);
        grounded = true;
        //smoke = GetComponentInChildren<ParticleSystem>();
        //Debug.Log(smoke.ToString());
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(jumpForce, grounded);
            anim.SetBool("Grounded", false);
        }
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        direction = new Vector2 (moveX, moveY);
        Run(direction);

        if (playerRb.velocity ==  Vector2.zero)
        {
            anim.SetBool("Running", false);
            startedRunning = false;
        }

        

        /*if (playerRb.velocity.x == 0f)
        {
            if (moveX < 0f)
            {
                smoke.transform.rotation = new Quaternion (0, 0, 180, 0);
            }

            else
            {
                smoke.transform.rotation = Quaternion.identity;
            }
            smoke.Play();
        }*/
    }

    private void Jump(float jumpForce, bool onGround)
    {
       // playerRb.velocity = new Vector2(0, jumpVelocity) * Time.deltaTime; 
        if (onGround == true)
        {
            playerRb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    private void Run(Vector2 direction)
    {
        if (moveX < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            anim.SetBool("Running", true);
            
        }

        if (moveX > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            anim.SetBool("Running", true);
            
        }
        playerRb.velocity = new Vector2(direction.x * runSpeed * Time.deltaTime, playerRb.velocity.y);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void SetJumpAnimation(bool grounded)
    {
        anim.SetBool("Grounded", grounded);
    }

}
