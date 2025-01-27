using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ShootManger : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 20f;
        public Camera mainCamera;
        public RectTransform aimCursor;
        public Transform Projektilleft1, Projektilleft2, Projektilleft3, Projektilright1, Projektilright2, Projektilright3;
        public List<Transform> leftSide, rightSide;
        public GameObject smoke;
        public AudioSource audioSource;
        public float shootingInterval = 3f;
        private float shootingTimer;

        Pause pause;
        private void Start()
        {
            UnityEngine.Cursor.visible = false;
            leftSide.Add(Projektilleft1);
            leftSide.Add(Projektilleft2);
            leftSide.Add(Projektilleft3);
            rightSide.Add(Projektilright1);
            rightSide.Add(Projektilright2);
            rightSide.Add(Projektilright3);
            pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        }
        void Update()
        {
            if (!pause.pause)
            {
                Vector3 mousePosition = Input.mousePosition;
                aimCursor.position = mousePosition;

                shootingTimer += Time.deltaTime;
                if (Input.GetMouseButtonDown(0))
                {
                    if (PlayerPrefs.GetInt("Ammo") > 0 && shootingTimer >= shootingInterval)
                    {
                        Shoot();
                        shootingTimer = 0f;
                    }
                }
            }
        }

        void Shoot()
        {
            audioSource.Play();
            PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo") - 1);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;

                Transform projectileOrigin;
                if (aimCursor.transform.localPosition.x < 0)
                {
                    projectileOrigin = leftSide[Random.Range(0, 3)];
                }
                else
                {
                    projectileOrigin = rightSide[Random.Range(0, 3)];
                }
                Instantiate(smoke, projectileOrigin.position, Quaternion.identity);
                GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.identity);

                Vector3 direction = (hitPoint - projectileOrigin.position).normalized;

                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }
            }
        }
    }
}