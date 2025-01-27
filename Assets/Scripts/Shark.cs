using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Health") > 0)
        {
            this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 21);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 7, Space.World);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Health", 0);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Bomb") || other.gameObject.CompareTag("BombWall"))
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Health", 0);
            Destroy(collision.gameObject);
        }
    }
}
