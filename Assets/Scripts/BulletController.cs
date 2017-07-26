using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float initialSpeed = 2;
    public float lifeSpan = 3;

    void Update()
    {
        OffScreenCheck();
    }

    public void Fire(Vector2 direction)
    {
        gameObject.SetActive(true);
        AudioManager.PlayEffect("GunShot", 1, 1);
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = direction * initialSpeed;
        StartCoroutine("LifecycleCoroutine");
    }

    IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(lifeSpan);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
        }
    }
    void OffScreenCheck()
    {
        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
        if (view.x > 1)
        {
            gameObject.SetActive(false);
        }
    }
}
