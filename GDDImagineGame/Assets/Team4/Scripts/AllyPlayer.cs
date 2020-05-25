using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllyPlayer : Player
{
    private static int AllyScore;

    protected void Start()
    {
        AllyScore = 0;
        base.Start();
    }
    protected void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                // Death method found in Player.cs
                InitDeath();
                break;
            case "Player":
            case "Bullet":
                //do nothing
                break;
            case "KillConfirm":
                AllyScore += KillConfirm(collider);
                Debug.Log("AllyScore: " + AllyScore);
                break;
            default:
                base.OnTriggerEnter(collider);
                break;
        }
    }
}
