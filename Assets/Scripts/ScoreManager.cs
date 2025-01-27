using Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private int pscore;
    // Start is called before the first frame update
    public void AddNewScore(string pname)
    {
        PlayerMovement palyerobject = FindObjectOfType<PlayerMovement>();
        pscore = palyerobject.score;
        if (pname == null || pname == "") 
        {
            return;
        }
        else if(pname != "")
        {
            PlayerPrefs.SetString("SavedScores", PlayerPrefs.GetString("SavedScores") + pname + "," + pscore.ToString() + ";");
            PlayerPrefs.Save();
        }
    }

    public void ClearallScores()
    {
        PlayerPrefs.DeleteAll();
    }
}
public class Score : MonoBehaviour
{
    public string PlayerName;
    public int PlayerScore;

    public Score(string pname, int pscore)
    {
        PlayerName = pname;
        PlayerScore = pscore;
    }
}
