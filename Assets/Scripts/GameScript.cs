using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class GameScript : MonoBehaviour
{
    public System.Random random = new System.Random();
    public bool isRight;
    public Text ScoreText;
    public Text SideText;
    public Text CountdownText;
    public bool GameActive = false;
    public Text stopwatch;
    public double totaltime = 0;
    public double avgtime;
    public int score = 0;
    public Image rightimage, leftimage;

    public Stopwatch timer;

    public AudioSource source;
    public AudioClip scoreSound;
    public AudioClip failSound;

    //if rightside then true if left then false

    // Start is called before the first frame update
    void Start()
    {
        CountdownText.enabled = true;
        GameActive = false;
        SideText.enabled = false;
        ScoreText.enabled = false;
        stopwatch.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        rightimage.enabled = false;
        leftimage.enabled = false;

        timer = new Stopwatch();

        StartCoroutine(Countdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameActive)
        {
            if (Input.GetKeyDown("space"))
            {
                EndGame();
            }
        }

        if (timer.IsRunning)
        {
            double seconds = timer.Elapsed.TotalSeconds;

            stopwatch.text = string.Format("{0:N2}", seconds);
        }
    }

    public void setSide()
    {
        isRight = random.Next(2) == 1;
        if (isRight) { SideText.text = "right"; }
        else { SideText.text = "left"; }

        timer.Reset();
        timer.Start();
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count >= 0)
        {
            CountdownText.text = count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }
        CountdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        StartGame();
    }

    public void addScore()
    {
        timer.Stop();
        UnityEngine.Debug.Log(timer.ElapsedMilliseconds);
        score += 1;
        ScoreText.text = score.ToString();
        playSound(source, scoreSound, 0.5f);
        updateAvg(timer.Elapsed.TotalSeconds);
        setSide();
    }

    public void StartGame()
    {
        CountdownText.enabled = false;
        GameActive = true;
        SideText.enabled = true;
        ScoreText.enabled = true;
        stopwatch.enabled = true;
        rightimage.enabled = true;
        leftimage.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
        setSide();
    }

    public void EndGame()
    {
        if (PlayerPrefs.HasKey("lowavgtime"))
        {
            if (avgtime < double.Parse(PlayerPrefs.GetString("lowavgtime")) && score >= PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetString("lowavgtime", avgtime.ToString());
            }
        }
        else
        {
            PlayerPrefs.SetString("lowavgtime", avgtime.ToString());
        }

        if (PlayerPrefs.HasKey("highscore"))
        {
            if (score > (PlayerPrefs.GetInt("highscore")))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            else { }
            UnityEngine.Debug.Log(PlayerPrefs.GetInt("highscore"));
        }
        else
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.SetString("lowavgtime", avgtime.ToString());
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("StartScreen");
    }

    public void playSound(AudioSource source, AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
    }

    public void updateAvg(double currTime)
    {
        totaltime += currTime;
        avgtime = totaltime / score;
        avgtime = Math.Truncate(avgtime * 100) / 100;
    }

    public void ResetScore()
    {
        timer.Stop();
        UnityEngine.Debug.Log(timer.ElapsedMilliseconds);
        score = 0;
        ScoreText.text = score.ToString();
        playSound(source, failSound, 0.5f);
        setSide();
    }

}