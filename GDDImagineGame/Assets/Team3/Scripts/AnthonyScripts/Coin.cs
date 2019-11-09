﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collidable
{
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            gameManager.AddPoints(other, 1);
        }
    }
}
