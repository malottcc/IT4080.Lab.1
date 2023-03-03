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
    public float speed = 18.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 18.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Vector2 turn;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
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
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }


}


    /*if (Input.GetButtonDown("Fire1"))
    {
        RequestNextColorServerRpc();
    }*/





    //----------
    //Color

    /*private static Color[] availColors = new Color[] {
            Color.black, Color.blue, Color.cyan,
            Color.gray, Color.green, Color.yellow };
    private int hostColorIndex = 0;
    public NetworkVariable netPlayerColor = new NetworkVariable();


    public override void OnNetworkSpawn()
    {
        netPlayerColor.OnValueChanged += OnPlayerColorChanged;
    }


    public void ApplyPlayerColor()
    {
        GetComponent().material.color = netPlayerColor.Value;
    }


    public void OnPlayerColorChanged(Color previous, Color current)
    {
        ApplyPlayerColor();
    }

    [ServerRpc]
    void RequestNextColorServerRpc(ServerRpcParams serverRpcParams = default)
    {
        hostColorIndex += 1;
        if (hostColorIndex > availColors.Length - 1)
        {
            hostColorIndex = 0;
        }

        Debug.Log($"host color index = {hostColorIndex} for {serverRpcParams.Receive.SenderClientId}");
        netPlayerColor.Value = availColors[hostColorIndex];
    }*/

