using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Diagnostics;

public class Bullet : NetworkBehaviour
{
    [SerializeField]

    private float speed = 20f;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Destroy(gameObject);

            if (IsOwner)
            {
                

            }

            if (!IsOwner)
            {

            }
        }
    }


}
