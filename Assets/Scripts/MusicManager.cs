using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] Songs;
    public AudioSource audio;
    private Pause pause;
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = Songs[0];
        audio.Play();
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.pause)
        {
            if (!audio.isPlaying)
            {
                audio.clip = Songs[Random.Range(0, Songs.Length)];
                audio.Play();
            }
        }
    }
}
