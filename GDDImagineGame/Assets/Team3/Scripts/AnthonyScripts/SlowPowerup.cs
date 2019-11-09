using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPowerup : Collidable
{
    public float timeLeft = 2;

    private bool isActive = false;
    private GameObject activePlayer;
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            activePlayer = other;
            other.GetComponent<Movement>().multiplier = 5;
            isActive = true;
            timeLeft = 2;
        }
    }

    private void Update()
    {
        if (isActive == true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                activePlayer.GetComponent<Movement>().multiplier = 10;
                isActive = false;
                timeLeft = 2;
            }
        }
    }
}
