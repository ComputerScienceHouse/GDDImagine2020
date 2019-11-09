using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collidable
{
    public override void OnCollide(GameObject other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find("DoubleCoins").GetComponent<DoubleCoinsPowerup>().isActive == true)
            {
                gameManager.AddPoints(other, 2);
            }
            else
            {
                gameManager.AddPoints(other, 1);
            }
            
        }
        Destroy(gameObject);
    }
}
