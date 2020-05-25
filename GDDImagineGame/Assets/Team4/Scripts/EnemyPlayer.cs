using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPlayer : Player
{
    private static int EnemyScore;

    protected void Start()
    {
        EnemyScore = 0;
        base.Start();
    }
    protected void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                //do nothing
                break;
            case "Player":
                //Ally death handled in AllyPlayer.cs
                break;
            case "KillConfirm":
                EnemyScore += KillConfirm(collider);
                Debug.Log("EnemyScore: " + EnemyScore);
                break;
            case "Bullet":
                InitDeath();
                break;
            case "Dot":
                EnemyScore += 1;
                base.OnTriggerEnter(collider);
                break;
            default:
                base.OnTriggerEnter(collider);
                break;
        }
    }
}
