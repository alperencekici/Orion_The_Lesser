using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public GameObject flame1, flame2, flame3, flame4;
    public AudioSource audioSource;
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerHit") == 1)
        {
            audioSource.Play();
            PlayerPrefs.SetInt("PlayerHit", 0);
        }
        if (PlayerPrefs.GetInt("Health") == 5)
        {
            flame1.SetActive(false);
            flame2.SetActive(false);
            flame3.SetActive(false);
            flame4.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Health") == 4)
        {
            flame1.SetActive(true);
            flame2.SetActive(false);
            flame3.SetActive(false);
            flame4.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Health") == 3)
        {
            flame1.SetActive(true);
            flame2.SetActive(true);
            flame3.SetActive(false);
            flame4.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Health") == 2)
        {
            flame1.SetActive(true);
            flame2.SetActive(true);
            flame3.SetActive(true);
            flame4.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Health") <= 1)
        {
            flame1.SetActive(true);
            flame2.SetActive(true);
            flame3.SetActive(true);
            flame4.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Health") <= 0)
        {
            Debug.Log("You Are Dead");
        }
    }
}