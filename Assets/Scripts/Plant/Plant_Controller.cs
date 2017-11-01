﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Controller : MonoBehaviour {

    /// <summary>
    /// All of this is temp
    /// </summary>

    private int growmultiplicator;
    private Plant[] plants;

    private void Awake()
    {
        plants = FindObjectsOfType<Plant>();
    }

    private void ReceiveHeight(int growThisMuch)
    {
        plants[0].Grow(growThisMuch);
    }

    public void ReceiveLove(Phrase phrase)
    {
        growmultiplicator = plants[0].CheckLove(phrase.loveType);

        int newHeight = phrase.love * growmultiplicator;
        ReceiveHeight(newHeight);
    }

    private void ReceivePowerUp()
    {

    }

    private void ReceiveClimateAvertion(int ungrow)
    {

    }
}
