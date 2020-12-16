using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;
    public float SteeringDirection;
    public float Steering;

    public float BoostMultiplier = 1;
    public int Input_pulse_count;
    float currentMoveRatio = 0;

    private PlayerAnimationController playerAnimController;
    private AnimatorStateInfo animatorState;

    public float moveHorizontal;
    public float moveVertical;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerAnimController = GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.W))// Paddle Pressed
            {
                StartCoroutine(PushSpeed());
            }
            else
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * GameManager.instance.Accelaration / 2);
            }

            if (Input.GetKey(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.A))// Left Button Pressed
            {
                SteeringDirection = -1;
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * GameManager.instance.Accelaration / 2);
            }
            else if (Input.GetKey(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.D))// Right Button Pressed
            {
                SteeringDirection = 1;
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * GameManager.instance.Accelaration / 2);
            }
            else
            {
                SteeringDirection = 0;
            }

            //moveVertical = Mathf.Clamp(moveVertical, 0.4f, 1f);

            //Vector3 movePosition = new Vector3(moveHorizontal, 0f, moveVertical);
            //transform.Translate(movePosition * speed * Time.deltaTime);

            //if (this.transform.position.x < -2f)
            //{
            //    Vector3 pos = this.transform.position;
            //    pos.x = -2f;
            //    this.transform.position = pos;
            //}

            //if (this.transform.position.x > 2f)
            //{
            //    Vector3 pos = this.transform.position;
            //    pos.x = 2f;
            //    this.transform.position = pos;
            //}

            //if (moveVertical < 0f)
            //{
            //    moveVertical = 0.1f;
            //}
        }

        //transform.Rotate(Vector3.up * Time.deltaTime * SteeringDirection * Steering * (.5f + (speed / GameManager.instance.maxMovementSpeed)));
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        playerAnimController.SetDirection(SteeringDirection);

        float animatedMoveSpeed = speed / GameManager.instance.maxMovementSpeed;

        currentMoveRatio = Mathf.MoveTowards(currentMoveRatio, animatedMoveSpeed, Time.deltaTime / 3);

        if (!GameManager.instance.gameStarted)
        {
            playerAnimController.SetRidingSpeed(0);
        }
        else
        {
            playerAnimController.SetRidingSpeed(currentMoveRatio);
        }

        BoostMultiplier = Mathf.Lerp(BoostMultiplier, 1, Time.deltaTime / 5);
    }

    IEnumerator PushSpeed()
    {
        for (int i = 0; i < Input_pulse_count; i++)
        {
            speed = Mathf.Lerp(speed, GameManager.instance.maxMovementSpeed * BoostMultiplier, Time.deltaTime * 
                            GameManager.instance.Accelaration);

            yield return new WaitForSeconds(.1f);
        }
    }
}
