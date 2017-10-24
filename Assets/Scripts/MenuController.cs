using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public string level;
    public bool MenuScreen = false;

    private void OnEnable()
    {
        StartCoroutine("NextLevel");
    }

    IEnumerator NextLevel()
    {
        Debug.Log(name);
        yield return new WaitForSeconds(1);
        while (enabled)
        {
            Cursor.visible = false;
            if (Input.GetButton("Fire1"))
            {
                SceneManager.LoadScene(level);
            }
            if (Input.GetButton("Fire2") && MenuScreen == true)
            {
                Application.Quit();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
