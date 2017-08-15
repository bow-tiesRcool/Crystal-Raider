using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public AudioClip gameOver;
    public Text gameOverUI;
    public Text CrystalsLeft;
    public Text livesUI;
    public Text CaveComplete;
    public Text EnterCave;
    public string Player = "Player";
    public int lives = 1;
    public int crystals;
    public GameObject CaveDoor;
    //public int score = 0;
    //public int highScore;
    //public Text scoreUI;
    //public Text highScoreUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance.gameOverUI.gameObject.SetActive(false);
        }
    }

    IEnumerator Start()
    {
        Cursor.visible = false;
        livesUI.text = "Lives: " + lives;
        CrystalsLeft.text = "Crystals Left: " + instance.crystals;
        //scoreUI.text = "Score: " + score;
        //instance.highScoreUI.text = "HighScore: " + PlayerPrefs.GetInt("highScore");
        yield return new WaitForEndOfFrame();
        AudioManager.CrossFadeMusic(AudioManager.instance.music, 1);
    }

    //private void Update()
    //{
    //    HighScore();
    //}

    public static void GameOver()
    {
        Debug.Log("you lost");
        instance.gameOverUI.text = "Game Over";
        instance.gameOverUI.gameObject.SetActive(true);
        AudioManager.CrossFadeMusic(instance.gameOver, 1);
        //HighScoreSaver();
    }

    public static void CrystalCollected()
    {
        instance.crystals--;
        instance.CrystalsLeft.text = "Crystals Left: " + instance.crystals;
        if (instance.crystals == 0)
        {
            instance.CaveComplete.text = "Cave Complete!";
            instance.CaveComplete.gameObject.SetActive(true);
            instance.CaveDoor.SetActive(true);
        }
    }

    //public static void Points(int points)
    //{
    //    int score = points;
    //    instance.score += score;
    //    instance.scoreUI.text = "Score: " + instance.score;
    //}

    //public static void HighScore()
    //{
    //    if (instance.score > instance.highScore)
    //    {
    //        instance.highScore = instance.score;
    //    }
    //    if (instance.highScore > PlayerPrefs.GetInt("highScore"))
    //    {
    //        instance.highScoreUI.text = "highScore: " + instance.highScore;
    //    }
    //}
    //public static void HighScoreSaver()
    //{
    //    if (PlayerPrefs.HasKey("highScore") == true)
    //    {
    //        if (instance.highScore > PlayerPrefs.GetInt("highScore"))
    //        {
    //            int newHighScore = instance.highScore;
    //            PlayerPrefs.SetInt("highScore", newHighScore);
    //            PlayerPrefs.Save();
    //        }
    //    }
    //    else
    //    {
    //        int newHighScore = instance.highScore;
    //        PlayerPrefs.SetInt("highScore", newHighScore);
    //        PlayerPrefs.Save();
    //    }
    //}

    public static void LifeLost()
    {
        instance.lives = instance.lives - 1;
        instance.livesUI.text = "Lives: " + instance.lives;

        if (instance.lives < 0)
        {
            GameOver();
        }
        else
        {
            instance.livesUI.text = "Lives: " + instance.lives;
            instance.StartCoroutine("SpawnTimer");

        }
    }

    //public void DropPowerUp(Vector3 pos)
    //{
    //    Debug.Log("Spawning PowerUp");
    //    string PowerUpPrefabName = PowerUpPrefabNames[Random.Range(0, PowerUpPrefabNames.Length)];
    //    AudioManager.PlayEffect("Powerup11", 1, 1);
    //}

    //public static void AddLife()
    //{
    //    if (instance.lives < 5)
    //    {
    //        instance.lives = instance.lives + 1;
    //        instance.livesUI.text = "Lives: " + instance.lives;
    //    }
    //    else
    //    {
    //        Debug.Log("Max Lives");
    //    }
    //}

    //public IEnumerator Flash(float time, float interval)
    //{
    //    for (float i = 0; i < time; i += Time.deltaTime)
    //    {
    //        Debug.Log(i);
    //        PlayerController.instance.sprite.enabled = true;
    //        yield return new WaitForSeconds(interval);
    //        PlayerController.instance.sprite.enabled = false;
    //        yield return new WaitForSeconds(interval);
    //    }
    //    yield return PlayerController.instance.sprite.enabled = true;
    //}
}