using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10;
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
        float x = Input.GetAxis("Horizontal");
        body.velocity = new Vector2((x * speed), body.velocity.y);

        JumpRoutine();
        Grounded();	
	}

    void JumpRoutine()
    {
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
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
            jumpCount = maxJumpCount;
        }
    }
}
