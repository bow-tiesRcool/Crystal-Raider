using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    public float speed = 5;
    private Rigidbody2D body;
    public int points = 10;
    private Animator anim;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("EnemyMovement");
    }

    IEnumerator EnemyMovement()
    {
        while (enabled)
        {
            body.velocity = Vector2.left * speed;
            transform.right = Vector3.left;
            anim.SetBool("Move", true);
            anim.SetFloat("Speed", 1);
            yield return new WaitForSeconds(2);
            body.velocity = Vector2.right * speed;
            transform.right = Vector3.right;
            anim.SetBool("Move", true);
            anim.SetFloat("Speed", 1);
            yield return new WaitForSeconds(2);
            body.velocity = Vector2.zero;
            anim.SetBool("Move", false);
            anim.SetFloat("Speed", 0);
            yield return new WaitForSeconds(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            StopCoroutine("EnemyMovement");
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Death", false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            StopCoroutine("EnemyMovement");
            StartCoroutine("Death");
            c.gameObject.SetActive(false);
        }
    }
}