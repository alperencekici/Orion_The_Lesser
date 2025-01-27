using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite health100, health75, health50, health25, health1, health0;
    public List<Sprite> healthList;
    public Image Healthbar;
    public GameObject GameOverMenu, PauseMenu;
    public TextMeshProUGUI Ammo, Boost, Score, Money, Name;
    Pause pause;
    PlayerMovement palyerobject;
    ScoreManager scoremanager;
    // Start is called before the first frame update
    void Start()
    {

        scoremanager = FindObjectOfType<ScoreManager>();
        palyerobject = FindObjectOfType<PlayerMovement>();
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        healthList.Add(health0);
        healthList.Add(health1);
        healthList.Add(health25);
        healthList.Add(health50);
        healthList.Add(health75);
        healthList.Add(health100);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("Health") == 0)
        {
            GameOverMenu.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Health") > 0 && pause.pause)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
        }

        for (int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.GetInt("Health") == i)
            {
                Healthbar.sprite = healthList[i];
            }
        }
        Ammo.text = PlayerPrefs.GetInt("Ammo").ToString();
        Boost.text = PlayerPrefs.GetInt("Boost").ToString();
        Score.text = palyerobject.score.ToString();
        Money.text = PlayerPrefs.GetInt("Money").ToString();
    }

    public void BacktoSea()
    {
        PauseMenu.SetActive(false);
        pause.pause = false;
        Cursor.visible = false;
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    public void Retry()
    {
        scoremanager.AddNewScore(Name.text);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    public void BacktoMainMenuSaved()
    {
        scoremanager.AddNewScore(Name.text);
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
