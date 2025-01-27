using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject main, Tutorial, Highscore;
    public GameObject[] TutorialList;
    public TextMeshProUGUI[] highscorenames, highscorescores;
    private int i = 0;
    public Texture2D cursor;
    private void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.SetCursor(cursor,Vector2.zero, CursorMode.Auto);
    }
    public void PlayClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    public void ExitClicked()
    {
        Application.Quit();
    }
    public void TutorialClicked()
    {
        Tutorial.SetActive(true);
        main.SetActive(false);
    }
    public void HighscoreClicked()
    {
        Highscore.SetActive(true);
        main.SetActive(false);
        List<Score> scores = new List<Score>();
        //getting player name and Score to string and integer arrays
        string list = PlayerPrefs.GetString("SavedScores");
        string[] splitter = list.Split(';');
        foreach (string s in splitter)
        {
            string[] splitter2 = s.Split(',');
            if (splitter2[0] != "")
            {
                scores.Add(new Score(splitter2[0], Convert.ToInt32(splitter2[1])));
                Debug.Log(splitter2[0] + " score: " + splitter2[1]);
            }
        }

        //sorting the scores from high to low
        //var temp;
        for (int j = 0; j <= scores.Count - 2; j++)
        {
            for (int i = 0; i <= scores.Count - 2; i++)
            {
                if (scores[i].PlayerScore < scores[i + 1].PlayerScore)
                {
                    var temp = scores[i + 1];
                    scores[i + 1] = scores[i];
                    scores[i] = temp;
                }
            }
        }
        PlayerPrefs.SetString("SavedScores", "");
        for (int i = 0; i < 5; i++)
        {
            highscorenames[i].text = scores[i].PlayerName;
            highscorescores[i].text = scores[i].PlayerScore.ToString();
            PlayerPrefs.SetString("SavedScores", PlayerPrefs.GetString("SavedScores") + scores[i].PlayerName + "," + scores[i].PlayerScore.ToString() + ";");
        }
        PlayerPrefs.Save();
    }
    public void nexttutorial()
    {
        if (i == TutorialList.Length - 1)
        {
            Tutorial.SetActive(false);
            main.SetActive(true);
            i = 0;
        }
        else
        {
            i += 1;
        }
    }
    public void backtutorial()
    {
        if (i == 0)
        {
            Tutorial.SetActive(false);
            main.SetActive(true);
        }
        else
        {
            i -= 1;
        }
    }
    public void backHighscore()
    {
        Highscore.SetActive(false);
        main.SetActive(true);
    }
    private void Update()
    {
        if (Tutorial.activeSelf)
        {
            for (int i = 0; i < TutorialList.Length; i++)
            {
                TutorialList[i].SetActive(false);
            }
            TutorialList[i].SetActive(true);
        }
    }
}
