using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float speed = 5;
    public float jumpForce = 1;
    public int maxJumpCount = 2;
    public int jumpCount = 1;
    public float move;
    bool onGround = true;
    private Rigidbody2D body;
    private Animator anim;
    bool walksound = false;
    public bool nearSign = false;
    Vector2 teleport;

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
        move = Input.GetAxis("Horizontal");
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

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
            GameObject bullet = Spawner.Spawn("Bullet");
            bullet.transform.position = PlayerController.instance.transform.position;
            bullet.GetComponent<BulletController>().Fire(transform.right);
           
        }

        if (Input.GetKeyDown(KeyCode.R) && nearSign == true)
        {
            Debug.Log("R was pressed");
            TextController.instance.Write();
        }
    }

    public static void JumpRoutine()
    {
        if (instance.jumpCount <= instance.maxJumpCount && instance.jumpCount >= 0)
        {
            instance.anim.SetBool("Jump", true);
            AudioManager.PlayEffect("Jump", 1, .3f);
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
        AudioManager.PlayEffect("Walk", 1f, .25f);
        yield return new WaitForSeconds(.5f);
        walksound = false;
    }

    IEnumerator Death()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Death", false);
        GameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Sign")
        {
            nearSign = true;
            TextController.instance.pressR.SetActive(true);
            TextController.instance.textStuff = c.gameObject.GetComponent<SignController>().textToDisplay;
        }

        if (c.gameObject.tag == "Exit")
        {
            GameManager.instance.EnterCave.text = "Exit Cave";
            GameManager.instance.EnterCave.gameObject.SetActive(true);
        }

        if (c.gameObject.tag == "Door")
        {
            teleport = c.gameObject.GetComponent<DoorController>().location;
            transform.position = teleport;
        }

        if (c.gameObject.tag == "RedCrystal")
        {
            StartCoroutine("Death");
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Sign")
        {
            nearSign = false;
            TextController.instance.pressR.SetActive(false);
        }

        if (c.gameObject.tag == "Exit")
        {
            GameManager.instance.EnterCave.gameObject.SetActive(false);
        }
    }
}
