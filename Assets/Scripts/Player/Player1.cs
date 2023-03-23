using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using TMPro;


namespace It4080
{
    public class Player1 : NetworkBehaviour
    {
        public float speed = 6.0f;
        public It4080.ScoreChange scorechange;
        private Camera camera;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //------------------------------
        //Server Move Players

        [ServerRpc]
        public void PlayerMovementServerRpc(Vector3 posChange, ServerRpcParams rpcParams = default)
        {
            transform.Translate(posChange);
        }


        //-------------------
        //PowerUpTrigger

        private void HostHandlePowerUpPickUp(Collider power)
        {
            Destroy(power.gameObject);
        }

        public void OnTriggerEnter(Collider collision)
        {
            if (IsOwner)
            {
                if (collision.gameObject.CompareTag("PowerUp"))
                {
                    HostHandlePowerUpPickUp(collision);
                }
            }

            if (!IsOwner)
            {
                if (collision.gameObject.CompareTag("PowerUp"))
                {
                    HostHandlePowerUpPickUp(collision);
                }
            }
        }

        //OnCollisionEnter

        

        //--------------
        //Update

        void Update()
        {
            if (!IsOwner) return;

            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;
            transform.Translate(speed * Time.deltaTime * moveDirection);

            if (IsOwner)
            {
                //PlayerMovementServerRpc(moveDirection);
            }

        }

    }
}
