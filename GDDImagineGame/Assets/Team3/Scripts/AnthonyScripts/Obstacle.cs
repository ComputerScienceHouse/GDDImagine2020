using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : Collidable
{
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find("Immune").GetComponent<ImmunePowerup>().isActive == false)
            {
                if (other.GetComponent<Movement>().Score > 0)
                    gameManager.SubtractPoints(other, 1);
            }
        }
    }
}
