using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : Collidable
{
    public int damage = 1;

    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<Movement>().isInvincible)
            {
                if (other.GetComponent<Movement>().Score > 0)
                    gameManager.SubtractPoints(other, damage);
            }
        }
    }
}
