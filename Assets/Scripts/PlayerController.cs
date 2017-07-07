using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float speed = 5;
    public float jumpForce = 1;
    public int maxJumpCount = 1;
    public int jumpCount = 1;
    bool onGround = true;
    private Rigidbody2D body;
    private Animator anim;

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
            StartCoroutine("Walk");
            transform.right = Vector3.right;
        }
        else if (move < 0)
        {
            StartCoroutine("Walk");
            transform.right = Vector3.left;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine("JumpRoutine");
        }
	}

    IEnumerator JumpRoutine()
    {
        if (instance.onGround == true)
        {
            instance.anim.SetBool("Jump", true);
            Debug.Log("Play Jump Sound");
            AudioManager.PlayEffect("Jump", 1, 1);
            Debug.Log("JumpSoundOver");
            instance.body.AddForce(Vector2.up * instance.jumpForce, ForceMode2D.Impulse);

            --instance.jumpCount;

            if (instance.jumpCount == 0)
            {
                instance.onGround = false;
            }

            if (instance.body.velocity.y >= 0 && instance.onGround == false)
            {
                instance.anim.SetBool("Jump", false);
                yield return StartCoroutine("Ground");
            }
        }
    }

    IEnumerator Ground()
    {
        AudioManager.PlayEffect("JumpLand", 1, 1);
        yield return new WaitForSeconds(2);
        instance.onGround = true;
        instance.jumpCount = instance.maxJumpCount;
        yield return new WaitForEndOfFrame();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Bat")
        {
            anim.SetBool("Death", true);
            //StartCoroutine("Death");
            GameManager.GameOver();
        }
    }

    IEnumerator Walk()
    {
        anim.SetBool("Walk", true);
        anim.SetFloat("Speed", 1);
        yield return new WaitForEndOfFrame();
        AudioManager.PlayEffect("Walk", 1, 1);
        yield return new WaitForSeconds(1);
    }

    //IEnumerator Death()
    //{
    //    anim.SetBool("Death", true);
    //    yield return new WaitForSeconds(1);
    //    anim.SetBool("Death", false);
    //}
}
