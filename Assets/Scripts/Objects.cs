using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Objects : MonoBehaviour
    {
        public GameObject explosion;
        public AudioSource audioSource;
        private GameObject player;
        public void Start()
        {
            this.gameObject.SetActive(true);
            player = GameObject.Find("Player").gameObject;
            Vector3 position = transform.position;
            position.x = UnityEngine.Random.Range(-10f, 10f);
            if (!this.gameObject.CompareTag("BombWall") && !this.gameObject.CompareTag("BigRock"))
            {
                transform.position = position;
            }
            if (this.gameObject.CompareTag("BigRock"))
            {
                transform.position = new Vector3(0, -3f, transform.position.z);
            }
        }
        private void Update()
        {
            if (this.gameObject.transform.position.z + 20 < player.transform.position.z)
            {
                Destroy(this.gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Bullet"))
            {
                if (this.gameObject.CompareTag("Bomb") || this.gameObject.CompareTag("BombWall"))
                {
                    audioSource = GameObject.Find("Bomb-Explode").gameObject.GetComponent<AudioSource>();
                    Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
                    audioSource.Play();
                    Destroy(this.gameObject);
                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                if (this.gameObject.CompareTag("Bomb") || this.gameObject.CompareTag("BombWall"))
                {
                    audioSource = GameObject.Find("Bomb-Explode").gameObject.GetComponent<AudioSource>();
                    Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
                    audioSource.Play();
                    Destroy(this.gameObject);
                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                if (this.gameObject.CompareTag("Bomb") || this.gameObject.CompareTag("BombWall"))
                {
                    audioSource = GameObject.Find("Bomb-Explode").gameObject.GetComponent<AudioSource>();
                    CameraShake shake = FindObjectOfType<CameraShake>();
                    shake.TriggerShake(0.8f, 0.5f);
                    if (PlayerPrefs.GetInt("Health") > 1)
                    {
                        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 2);
                    }
                    Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
                }
                else if (this.gameObject.CompareTag("Chest"))
                {
                    audioSource = GameObject.Find("Chest-Collect").gameObject.GetComponent<AudioSource>();
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 10);
                }
                else if (this.gameObject.CompareTag("Mermaid"))
                {
                    audioSource = GameObject.Find("Heal-Up-Collect").gameObject.GetComponent<AudioSource>();
                    if (PlayerPrefs.GetInt("Health") < 5)
                    {
                        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 1);
                    }
                }
                else if (this.gameObject.CompareTag("Booster"))
                {
                    audioSource = GameObject.Find("Boost-Collect").gameObject.GetComponent<AudioSource>();
                    PlayerPrefs.SetInt("Boost", PlayerPrefs.GetInt("Boost") + 1);
                }
                else if (this.gameObject.CompareTag("BallChest"))
                {
                    audioSource = GameObject.Find("Canon-Balls-Collect").gameObject.GetComponent<AudioSource>();
                    PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo") + 10);
                    if (PlayerPrefs.GetInt("Ammo") > 99)
                    {
                        PlayerPrefs.SetInt("Ammo", 99);
                    }
                }
                else if (this.gameObject.CompareTag("Iceberg"))
                {
                    audioSource = GameObject.Find("Iceberg-Crash").gameObject.GetComponent<AudioSource>();
                    CameraShake shake = FindObjectOfType<CameraShake>();
                    shake.TriggerShake(0.8f, 0.5f);
                    if (PlayerPrefs.GetInt("Health") > 2)
                    {
                        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 3);
                        Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
                        audioSource.Play();
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Health", 0);
                        audioSource.Play();
                    }
                }
                else if (this.gameObject.CompareTag("BigRock"))
                {
                    audioSource = GameObject.Find("BigRock-Crash").gameObject.GetComponent<AudioSource>();
                    CameraShake shake = FindObjectOfType<CameraShake>();
                    shake.TriggerShake(0.8f, 0.5f);
                    PlayerPrefs.SetInt("Health", 0);
                    Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
                }
                audioSource.Play();
                if (!this.gameObject.CompareTag("BigRock") && !this.gameObject.CompareTag("Iceberg"))
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}