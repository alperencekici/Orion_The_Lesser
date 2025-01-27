using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float boostspeed;
    public float moveSpeedcopy;
    public float leftRightSpeed;
    public float swSpeed;
    public float leftsideEnd;
    public float rightsideEnd;
    public GameObject Boat;
    public GameObject SWheel;
    public AudioSource booster;
    public float maxRotationAngle = 45f;
    public float rotationSpeed = 90f;
    private float time;
    public float timeinterval = 60f;
    private float currentRotation = 0f;
    public int score;
    Pause pause;
    private void Start()
    {
        pause = GameObject.Find("Pause").gameObject.GetComponent<Pause>();
        score = 0;
    }
    void Update()
    {
        Boat.transform.localPosition = new Vector3(0, Boat.transform.localPosition.y, Boat.transform.localPosition.z);
        if (!pause.pause)
        {
            score = Mathf.RoundToInt(this.gameObject.transform.position.z / 10);
            time += Time.deltaTime;
            if (time >= timeinterval)
            {
                if (moveSpeed >= 50)
                {
                    moveSpeedcopy = 50f;
                    moveSpeed = 50f;
                    boostspeed = moveSpeedcopy * 2;
                }
                else
                {
                    moveSpeedcopy += moveSpeedcopy * 0.10f;
                    moveSpeed += moveSpeed * 0.10f;
                    boostspeed = moveSpeedcopy * 2;
                }
                boostspeed = moveSpeed + 10f;
                if (leftRightSpeed >= 10)
                {
                    leftRightSpeed = 10f;
                }
                else
                {
                    leftRightSpeed += leftRightSpeed * 0.10f;
                    rotationSpeed += rotationSpeed * 0.10f;
                }
                time = 0f;
            }

            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > leftsideEnd)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                    RotateLeft();
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < rightsideEnd)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
                    RotateRight();
                }
            }
            else
            {
                ResetRotation();
            }

            if (PlayerPrefs.GetInt("Boost") > 0 && Input.GetKey(KeyCode.Space) && PlayerPrefs.GetInt("ActiveBoost") == 0)
            {
                booster.Play();
                PlayerPrefs.SetInt("Boost", PlayerPrefs.GetInt("Boost") - 1);
                PlayerPrefs.SetInt("ActiveBoost", 1);
            }

            if (PlayerPrefs.GetInt("ActiveBoost") == 1)
            {
                moveSpeed = boostspeed;
            }
            else
            {
                moveSpeed = moveSpeedcopy;
            }

            void RotateLeft()
            {
                float rotationStep = rotationSpeed * Time.deltaTime;

                if (currentRotation - rotationStep < -maxRotationAngle)
                {
                    rotationStep = currentRotation + maxRotationAngle;
                }

                Boat.transform.Rotate(Vector3.up, -rotationStep);
                SWheel.transform.Rotate(Vector3.back, rotationStep * swSpeed);
                currentRotation -= rotationStep;
            }

            void RotateRight()
            {
                float rotationStep = rotationSpeed * Time.deltaTime;

                if (currentRotation + rotationStep > maxRotationAngle)
                {
                    rotationStep = maxRotationAngle - currentRotation;
                }

                Boat.transform.Rotate(Vector3.up, rotationStep);
                SWheel.transform.Rotate(Vector3.back, -rotationStep * swSpeed);
                currentRotation += rotationStep;
            }

            void ResetRotation()
            {
                if (currentRotation > 0)
                {
                    float rotationStep = rotationSpeed * Time.deltaTime;
                    if (currentRotation - rotationStep < 0)
                    {
                        rotationStep = currentRotation;
                    }
                    Boat.transform.Rotate(Vector3.up, -rotationStep);
                    SWheel.transform.Rotate(Vector3.back, rotationStep * swSpeed);
                    currentRotation -= rotationStep;
                }
                else if (currentRotation < 0)
                {
                    float rotationStep = rotationSpeed * Time.deltaTime;
                    if (currentRotation + rotationStep > 0)
                    {
                        rotationStep = -currentRotation;
                    }
                    Boat.transform.Rotate(Vector3.up, rotationStep);
                    SWheel.transform.Rotate(Vector3.back, -rotationStep * swSpeed);
                    currentRotation += rotationStep;
                }
            }
        }
    }
}