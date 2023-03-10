using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;



public class Player1 : NetworkBehaviour
{
    public float speed = 8.0f;
    public float jumpSpeed = 3.0f;
    public float gravity = 10.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Vector2 turn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //------------------------------
    //Server Move Players

    


    //-------------------
    //PowerUpTrigger

    private void HostHandlePowerUpPickUp(Collider other)
    {
        Destroy(other.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (IsOwner)
        {
            if (other.gameObject.CompareTag("PowerUp"))
            {
                HostHandlePowerUpPickUp(other);
            }
        }

        if (!IsOwner)
        {
            if (other.gameObject.CompareTag("PowerUp"))
            {
                HostHandlePowerUpPickUp(other);
            }
        }
    }


    //--------------
    //Update

    void Update()
    {
        if (!IsOwner)
        {
            OwnerUpdate();
        }
    }

    void OwnerUpdate()
    {
        //Move
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime * speed);
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }


}
