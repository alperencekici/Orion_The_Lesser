using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CameraShake : MonoBehaviour
    {
        // Intensit�t des Sch�ttelns
        public float shakeIntensity = 0.3f;
        // Dauer des Sch�ttelns
        public float shakeDuration = 0.5f;

        private Vector3 originalPosition;

        //BoostingAnim
        public float boostFOV = 90f;             // Sichtfeld w�hrend des Boosts
        public float normalFOV = 60f;           // Normales Sichtfeld
        public float transitionSpeed = 5f;      // Geschwindigkeit der �bergangsanimation
        public float boostDuration = 5f;        // Dauer des Boosts in Sekunden

        // Interne Variablen
        private bool isBoosting = false;
        private float boostTimer = 0f;

        void Start()
        {
            // Die urspr�ngliche Position der Kamera speichern
            if (Camera.main != null)
            {
                originalPosition = Camera.main.transform.localPosition;
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //TriggerShake();
            }

            if (PlayerPrefs.GetInt("ActiveBoost") == 1 && !isBoosting)
            {
                StartBoost();
            }
            if (isBoosting)
            {
                boostTimer -= Time.deltaTime;

                // Boost beenden, wenn der Timer abl�uft
                if (boostTimer <= 0f)
                {
                    EndBoost();
                }
            }
            // Sichtfeld dynamisch anpassen
            UpdateCameraFOV();
        }

        void StartBoost()
        {
            isBoosting = true;
            boostTimer = boostDuration;
        }
        void EndBoost()
        {
            isBoosting = false;
            PlayerPrefs.SetInt("ActiveBoost", 0);
        }

        void UpdateCameraFOV()
        {
            // Ziel-FOV basierend auf dem Boost-Status
            float targetFOV = isBoosting ? boostFOV : normalFOV;

            // �bergang zwischen aktuellem und Ziel-FOV
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
        }

        // Methode zum Starten des Sch�ttelns
        public void TriggerShake(float intesity, float duration)
        {
            shakeIntensity = intesity;
            duration = shakeDuration;
            if (Camera.main != null)
            {
                StartCoroutine(Shake());
            }
        }

        private IEnumerator Shake()
        {
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;

                // Zuf�llige Verschiebung der Kamera
                Vector3 randomOffset = new Vector3(
                    Random.Range(-1f, 1f) * shakeIntensity,
                    Random.Range(-1f, 1f) * shakeIntensity,
                    0f //originalPosition.z // Z bleibt gleich, da wir die Tiefe nicht �ndern wollen
                );

                Camera.main.transform.localPosition = originalPosition + randomOffset;

                yield return null;
            }

            // Zur�ck zur urspr�nglichen Position
            Camera.main.transform.localPosition = originalPosition;
        }
    }
}