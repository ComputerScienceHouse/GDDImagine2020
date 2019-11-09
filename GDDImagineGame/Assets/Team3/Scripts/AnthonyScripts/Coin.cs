using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collidable
{
    public override void OnCollide(GameObject other)
    {
        GameObject.Find("Player").GetComponent<CharacterController>().score += 1;
        GameObject.Find("PointText").GetComponent<Text>().text = "Points: " + GameObject.Find("Player").GetComponent<CharacterController>().score;
    }
}
