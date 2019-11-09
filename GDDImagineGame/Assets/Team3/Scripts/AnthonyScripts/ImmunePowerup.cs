using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunePowerup : Collidable
{
    public float timeLeft = 2;

    public bool isActive = false;
    private GameObject activePlayer;
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            activePlayer = other;
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
            if (timeLeft <= 0)
            {
                isActive = false;
                timeLeft = 2;
            }
        }
    }
}
