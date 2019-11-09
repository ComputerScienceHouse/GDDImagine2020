using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlPowerup : Collidable
{
    public float timeLeft = 2;

    private bool isActive = false;
    private GameObject activePlayer;
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            activePlayer = other;
            other.GetComponent<Movement>().multiplier *= -1;
            isActive = true;
            timeLeft = 2;
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void Update()
    {
        if (isActive == true)
        {
            timeLeft -= Time.deltaTime;
            if (activePlayer.GetComponent<Movement>().multiplier > 0)
            {
                activePlayer.GetComponent<Movement>().multiplier *= -1;
            }
            if (timeLeft <= 0)
            {
                activePlayer.GetComponent<Movement>().multiplier *= -1;
                isActive = false;
                timeLeft = 2;
            }
            
        }
    }
}