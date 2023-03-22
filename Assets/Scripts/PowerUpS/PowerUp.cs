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
    public class PowerUp : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(0f, 50f * Time.deltaTime, 0f, Space.Self);
        }
    }
}
