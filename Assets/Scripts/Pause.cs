using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !pause || Input.GetKey(KeyCode.P) && !pause || PlayerPrefs.GetInt("Health") == 0)
        {
            StartCoroutine(StartPause());
        }
        if (Input.GetKey(KeyCode.Escape) && pause && PlayerPrefs.GetInt("Health") > 0 || Input.GetKey(KeyCode.P) && pause && PlayerPrefs.GetInt("Health") > 0)
        {
            StartCoroutine(EndPause());
        }
        if (!pause)
        {
            Cursor.visible = false;
        }
    }
    public IEnumerator StartPause()
    {
        yield return new WaitForSeconds(0.3f);
        pause = true;
        Cursor.visible = true;
    }
    public IEnumerator EndPause()
    {
        yield return new WaitForSeconds(0.3f);
        pause = false;
    }
}
