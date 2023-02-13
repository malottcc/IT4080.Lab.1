using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System;
using System.Net;

public class ChatUI : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            SendChatMessageClientRpc("I am the host, Whoop Whoop!");
        }
        else
        {
            SendChatMessageServerRpc("I am a client, Yay!");
        }
    }

    [ClientRpc]
    public void SendChatMessageClientRpc(string message, ClientRpcParams clientRpcParams = default)
    {
        Debug.Log(message);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendChatMessageServerRpc(string message, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log($"Host got message: {message}");
    }

}
