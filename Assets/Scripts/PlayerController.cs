using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float speed = 5;
    public float jumpForce = 1;
    public int maxJumpCount = 2;
    public int jumpCount = 1;
    bool onGround = true;
    private Rigidbody2D body;
    private Animator anim;
    bool walksound = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
        }
    }

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
            if (walksound == false && onGround == true)
            {
                StartCoroutine("Walk");
            }
            transform.right = Vector3.right;
        }
        else if (move < 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Speed", 1);
            if (walksound == false && onGround == true)
            {
                StartCoroutine("Walk");
            }
            transform.right = Vector3.left;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetButtonDown("Jump") && instance.jumpCount <= instance.maxJumpCount)
        {
            --jumpCount;
            JumpRoutine();
        }

        if (onGround == true)
        {
            Ground();
        }

        Debug.Log(onGround);
    }

    public static void JumpRoutine()
    {
        if (instance.jumpCount <= instance.maxJumpCount && instance.jumpCount >= 0)
        {
            instance.anim.SetBool("Jump", true);
            AudioManager.PlayEffect("Jump", 1, 1);
            instance.body.AddForce(Vector2.up * instance.jumpForce, ForceMode2D.Impulse);
            instance.onGround = false;
        }
    }

    public static void Ground()
    {
        if (instance.onGround == true)
        {
            instance.anim.SetBool("Jump", false);
            instance.jumpCount = instance.maxJumpCount;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Bat")
        {
            //anim.SetBool("Death", true);
            StartCoroutine("Death");
            GameManager.GameOver();
        }
        if(c.gameObject.tag == "Ground")
        {
            AudioManager.PlayEffect("JumpLand", 1, 1);
            onGround = true;
        }
    }

    IEnumerator Walk()
    {
        walksound = true;
        AudioManager.PlayEffect("Walk", 1, 1);
        yield return new WaitForSeconds(.5f);
        walksound = false;
    }

    IEnumerator Death()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Death", false);
        gameObject.SetActive(false);
        GameManager.GameOver();
    }
}
