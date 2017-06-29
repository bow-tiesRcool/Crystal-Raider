using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public float jumpForce = 1;
    public int maxJumpCount = 1;
    public int jumpCount = 1;
    bool onGround = true;
    private Rigidbody2D body;
    private Animator anim;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();	
	}
	
	void Update ()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 v = body.velocity;
        body.velocity = new Vector2((move * speed), body.velocity.y);
        if (move > 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Speed", 1);
            transform.right = Vector3.right;
        }
        else if (move < 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Speed", 1);
            transform.right = Vector3.left;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetFloat("Speed", 0);
        }

        JumpRoutine();
        Grounded();	
	}

    void JumpRoutine()
    {
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            anim.SetBool("Jump", true);
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            --jumpCount;
            if(jumpCount == 0)
            {
                onGround = false;
            }
        }
    }

    void Grounded()
    {
        if (body.velocity.y == 0)
        {
            onGround = true;
            anim.SetBool("Jump", false);
            jumpCount = maxJumpCount;
        }
    }
}
