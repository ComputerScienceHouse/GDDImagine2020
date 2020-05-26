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
                KillConfirm(collider);
                break;
            case "Dot":
                base.OnTriggerEnter(collider);
                break;
            default:
                base.OnTriggerEnter(collider);
                break;
        }
        Debug.Log("personal=" + localScore + " : enemyteam=" + EnemyScore);
    }

    protected override Material setMaterial(PlayerState id)
    {
        if (id.Equals(PlayerState.DEAD))
        {
            return DEAD_MATERIAL;
        }
        else
        {
            return LIVE_MATERIAL;
        }
    }
    protected override void Shoot(string controllerNum) { /* Enemy cannot shoot */ }

    public override void setTeamScore(int score)
    {
        EnemyScore += score;
    }
}
