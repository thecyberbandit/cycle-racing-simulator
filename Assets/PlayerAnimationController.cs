using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator playerAnim;
    float currentDirection = 0;


    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void SetRidingSpeed(float ridingSpeed)
    {
        playerAnim.SetFloat("Vertical", ridingSpeed);
    }
    
    public void SetDirection(float Direction)
    {
        currentDirection = Mathf.Lerp(currentDirection, Direction, Time.deltaTime * 10);
        playerAnim.SetFloat("Horizontal", currentDirection);
    }
}
