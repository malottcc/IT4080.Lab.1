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

    private void HostHandlePowerUpPickUp(Collider power)
    {
        Destroy(power.gameObject);
    }

    public void OnTriggerEnter(Collider power)
    {
        if (IsOwner)
        {
            if (power.gameObject.CompareTag("PowerUp"))
            {
                HostHandlePowerUpPickUp(power);
            }
        }

        if (!IsOwner)
        {
            if (power.gameObject.CompareTag("PowerUp"))
            {
                HostHandlePowerUpPickUp(power);
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
