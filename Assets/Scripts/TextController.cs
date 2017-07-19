using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public static TextController instance;

    Queue<IEnumerator> queue = new Queue<IEnumerator>();

    public Text textUI;
    public GameObject textBox;
    public GameObject pressR;

    public string[] textStuff;

    bool rPressed = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            pressR.SetActive(false);
            textBox.SetActive(false);
            textUI.text = "";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        StartCoroutine("QueueReader");
    }

    public void Start()
    {
        for (int i = 0; i < textStuff.Length; i++)
        {
            TextController.TypeText(textStuff[i]);
            TextController.WaitForInput();
            TextController.ClearText();
        }

        //TextController.ShowText("Blah Text");
        //TextController.TypeText("\nWaitForInput");
        //TextController.WaitForInput();
        //TextController.ClearText();
        //TextController.TypeText("Even more text");
        //TextController.WaitForInput();
    }

    public void Write()
    {
        pressR.SetActive(false);
        for (int i = 0; i < textStuff.Length; i++)
        {
            TextController.TypeText(textStuff[i]);
            TextController.WaitForInput();
            TextController.ClearText();
        }
        PlayerController.instance.nearSign = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) rPressed = true;
    }

    IEnumerator TypeTextCoroutine(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            textUI.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ShowTextCoroutine(string str)
    {
        textUI.text += str;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator WaitForInputCoroutine()
    {
        rPressed = false;
        pressR.SetActive(true);
        while (!rPressed)
        {
            yield return new WaitForEndOfFrame();
        }
        pressR.SetActive(false);
    }

    IEnumerator ClearTextCoroutine()
    {
        textUI.text = "";
        yield return null;
    }

    IEnumerator ShowHide(bool show)
    {
        textBox.SetActive(show);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator QueueReader()
    {
        while (enabled)
        {
            if (queue.Count > 0)
            {
                textBox.SetActive(true);
                while (queue.Count > 0)
                {
                    yield return StartCoroutine(queue.Dequeue());
                }
                textBox.SetActive(false);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public static void ClearText()
    {
        instance.queue.Enqueue(instance.ClearTextCoroutine());
    }

    public static void TypeText(string str)
    {
        instance.queue.Enqueue(instance.TypeTextCoroutine(str));
    }

    public static void ShowText(string str)
    {
        instance.queue.Enqueue(instance.ShowTextCoroutine(str));
    }

    public static void WaitForInput()
    {
        instance.queue.Enqueue(instance.WaitForInputCoroutine());
    }

    public static void Show()
    {
        instance.queue.Enqueue(instance.ShowHide(true));
    }

    public static void Hide()
    {
        instance.queue.Enqueue(instance.ShowHide(false));
    }
}
