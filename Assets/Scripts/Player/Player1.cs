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
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Vector3 moveDirection = Vector3.zero;
    public Vector2 turn;
    public Quaternion rotation;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    //------------------------------
    //Player Movement over Server

    [ServerRpc(RequireOwnership = false)]
    public void PlayerMovementServerRpc(Vector3 positionChange, ServerRpcParams serverRpcParams = default)
    {
        transform.Translate(positionChange);
        //transform.Rotate(rotationChange); Quaternion rotationChange
    }

    void Update()
    {
        if (IsOwner)
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


        //-----------
        //Mouse Look
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        transform.localRotation = rotation;


        PlayerMovementServerRpc(moveDirection); 

    }


    //------------
    //Color

    /*
     *


     * CharacterController controller = GetComponent<CharacterController>();


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

        PlayerMovementServerRpc(moveDirection); 

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            RequestNextColorServerRpc();
        }


     private static Color[] availColors = new Color[] {
            Color.black, Color.blue, Color.cyan,
            Color.gray, Color.green, Color.yellow };
    private int hostColorIndex = 0;
    public NetworkVariable<Color> netPlayerColor = new NetworkVariable<Color>();


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
}
