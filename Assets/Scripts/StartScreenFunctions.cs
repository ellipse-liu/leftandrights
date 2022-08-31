using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenFunctions : MonoBehaviour
{
    public Text HighScore;
    public Text LowAvg;
    public Image byeImage;
    public Text seeyoutext;

    public GameObject startButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        byeImage.enabled = false;
        seeyoutext.enabled = false;

        HighScore.enabled  = true;
        LowAvg.enabled = true;

        startButton.SetActive(true);
        exitButton.SetActive(true);

        if (! PlayerPrefs.HasKey("highscore"))
        {
            HighScore.text = "Highscore: 0";
            LowAvg.text = "Lowest Avg time: ?";
        }

        else
        {
            HighScore.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();

            if (!PlayerPrefs.HasKey("lowavgtime"))
            {
                LowAvg.text = "Lowest Avg time: ?";
            }

            else
            {
                LowAvg.text = "Lowest Avg time: " + PlayerPrefs.GetString("lowavgtime");
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        seeyoutext.enabled = true;
        byeImage.enabled = true;
        HighScore.enabled = false;
        LowAvg.enabled = false;
        startButton.SetActive(false);
        exitButton.SetActive(false);
        StartCoroutine(goodbye());
    }

    public IEnumerator goodbye()
    {
        Debug.Log("bye");

        yield return new WaitForSeconds(2);

        Application.Quit();
    }

}
