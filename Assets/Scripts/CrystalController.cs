using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

    void Start()
    {
        GameManager.instance.crystals++;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameManager.CrystalCollected();
            gameObject.SetActive(false);
        }
    }
}
