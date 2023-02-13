using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUI : MonoBehaviour
{


    //------
    //RPCs

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
