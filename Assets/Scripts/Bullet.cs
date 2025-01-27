using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Bullet : MonoBehaviour
    {
        public GameObject explosion;
        public GameObject explosion2;
        public void Awake()
        {
            StartCoroutine(StartFiveSecondTimer());
        }
        public IEnumerator StartFiveSecondTimer()
        {
            yield return new WaitForSeconds(5f);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(explosion, this.gameObject.transform.position, explosion.transform.rotation);
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerPrefs.SetInt("PlayerHit", 1);
                CameraShake shake = FindObjectOfType<CameraShake>();
                shake.TriggerShake(0.3f, 0.5f);
                if (PlayerPrefs.GetInt("Health") > 0)
                {
                    PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
                }
                Instantiate(explosion2, this.gameObject.transform.position, explosion.transform.rotation);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                Instantiate(explosion2, this.gameObject.transform.position, explosion.transform.rotation);
                Destroy(other.gameObject);
            }
        }

    }
}