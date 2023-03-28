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
    public class ScoreChange : NetworkBehaviour
    {
        public TMP_Text worldText;
        public string changeScore;
        public int score = 0;


        void Start()
        {
            worldText.text = "Score: " + score;
        }

        public void SetScore()
        {
        }

    }
}
