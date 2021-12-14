using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desLane = 1;
    public float laneDis = 4;

    public float jumpForce;
    public float g = -20;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStarted)
            return;
        
        direction.z = forwardSpeed;


        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += g * Time.deltaTime;
        }

        direction.y += g * Time.deltaTime;

        if (SwipeManager.swipeRight)
        {
            desLane++;
            if (desLane == 3)
                desLane = 2;
        }
        
        if (SwipeManager.swipeLeft)
        {
            desLane--;
            if (desLane == -1)
                desLane = 0;
        }

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desLane == 0)
            targetPos += Vector3.left * laneDis;
        else if (desLane == 2)
            targetPos += Vector3.right * laneDis;

        //transform.position = Vector3.Lerp(transform.position, targetPos, 70 * Time.deltaTime);
        if (transform.position != targetPos)
        {
            Vector3 diff = targetPos - transform.position;
            Vector3 moveDir = diff.normalized * (25 * Time.deltaTime);
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("Fail");
            
        }
    }
    
}
