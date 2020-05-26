using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPlayer : Player
{
    private static int EnemyScore;

    public Material LIVE_MATERIAL;
    public Material DEAD_MATERIAL;

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
            case "Bullet":
                //Death handled by bullet
            case "KillConfirm":
                EnemyScore += KillConfirm(collider);
                Debug.Log("EnemyScore: " + EnemyScore);
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

    protected override Material setMaterial(string id)
    {
        if (id.Equals("dead"))
        {
            return DEAD_MATERIAL;
        }
        else
        {
            return LIVE_MATERIAL;
        }
    }
    protected override void Shoot(string controllerNum) { /* Enemy cannot shoot */ }
}
