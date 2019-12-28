using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collidable
{
    public int value = 1;

    public override void OnCollide(GameObject other)
    {
        Debug.Log("yo");
        if (other.tag == "Player")
        {
            gameManager.AddPoints(other, value * other.GetComponent<Movement>().coinMultiplier);
        }
        Destroy(gameObject);
    }
}
