﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped;

    private Animator anim;

    public Transform firePoint;
    public GameObject ninjaStar;

    public float shotDelay;
    private float shotDelayCounter;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private Rigidbody2D myrigidbody2D;

    void Start()
    {
        anim = GetComponent<Animator>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        if (grounded)
            doubleJumped = false;

        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // Make it jump!
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        // Set move velocity to 0
        moveVelocity = 0f;

        // If user goes right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Apply force to move right
            moveVelocity = moveSpeed;
        }
        // If user goes left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Apply force to move left
            moveVelocity = -moveSpeed;
        }

        if (knockbackCount <= 0)
        {
            myrigidbody2D.velocity = new Vector2(moveVelocity, myrigidbody2D.velocity.y);
        } else {
            if (knockFromRight)
                myrigidbody2D.velocity=new Vector2(-knockback, knockback);
            if (!knockFromRight)
                            myrigidbody2D.velocity=new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }

        // Set animation speed float value to the value of the players horiz velocity
        anim.SetFloat("Speed", Mathf.Abs(myrigidbody2D.velocity.x));

        if (myrigidbody2D.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (myrigidbody2D.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            }
        }
    }

    public void Jump()
    {
        myrigidbody2D.velocity = new Vector2(0, jumpHeight);
    }
}
