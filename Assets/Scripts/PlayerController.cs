using System.Collections;
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

        if (Input.GetButtonDown("Jump") && grounded)
        {
            // Make it jump!
            Jump();
        }

        if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        // Get movement from horizontal axis
        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");

        if (knockbackCount <= 0)
        {
            myrigidbody2D.velocity = new Vector2(moveVelocity, myrigidbody2D.velocity.y);
        }
        else
        {
            if (knockFromRight)
                myrigidbody2D.velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                myrigidbody2D.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }

        // Set animation speed float value to the value of the players horiz velocity
        anim.SetFloat("Speed", Mathf.Abs(myrigidbody2D.velocity.x));

        if (myrigidbody2D.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (myrigidbody2D.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetButton("Fire1"))
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            }
        }

        if (anim.GetBool("Sword"))
            anim.SetBool("Sword", false);

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("Sword", true);
        }
    }

    public void Jump()
    {
        myrigidbody2D.velocity = new Vector2(0, jumpHeight);
    }
}
