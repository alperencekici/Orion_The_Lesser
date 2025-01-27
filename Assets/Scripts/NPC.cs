using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public GameObject smoke; 
    public Transform shootingPoint;
    public Transform player;
    public float shootingRange = 10f;
    public float shootingInterval = 2f;
    public float projectileSpeed = 30f;
    public float spreadAngle = 5f;
    private float shootingTimer;
    public AudioSource audioSource;
    Pause pause;

    private void Start()
    {
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        player = GameObject.Find("Player").gameObject.transform;
        audioSource = GameObject.Find("NPC-Shoot").gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!pause.pause)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootingRange)
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                shootingTimer += Time.deltaTime;
                if (shootingTimer >= shootingInterval)
                {
                    ShootAtPlayer();
                    shootingTimer = 0f;
                }
            }
        }
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab != null && shootingPoint != null && player != null)
        {
            audioSource.Play();
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
            Instantiate(smoke, shootingPoint.position, Quaternion.identity);

            Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized;
            directionToPlayer = ApplySpread(directionToPlayer);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = directionToPlayer * projectileSpeed;
            }
            else
            {
                Debug.LogError("Das Projektil hat keinen Rigidbody!");
            }
        }
    }
    Vector3 ApplySpread(Vector3 direction)
    {
        float randomX = Random.Range(-spreadAngle, spreadAngle);
        float randomY = Random.Range(-spreadAngle, spreadAngle);

        Quaternion spreadRotation = Quaternion.Euler(randomX, randomY, 0);
        return spreadRotation * direction;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    
}
