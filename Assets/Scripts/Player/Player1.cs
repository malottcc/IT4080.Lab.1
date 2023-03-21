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
    public float speed = 6.0f;

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
        if (!IsOwner) return;

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;
        transform.Translate(speed * Time.deltaTime * moveDirection);

    }

}
