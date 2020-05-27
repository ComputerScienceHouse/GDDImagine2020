﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllyPlayer : Player
{
    private static int AllyScore;
    
    public float FireRate;
    public float NextFire;
    public float FireRange;

    public Material LIVE_MATERIAL;
    public Material DEAD_MATERIAL;

    [SerializeField]
    private GameObject BulletPrefab;


    protected void Start()
    {
        AllyScore = 0;

        FireRate = 2.0f;
        NextFire = 0.0f;
        FireRange = 5.0f;

        alliance = Alliance.ALLY;
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
                KillConfirm(collider);
                break;
            case "Dot":
                base.OnTriggerEnter(collider);
                break;
            default:
                base.OnTriggerEnter(collider);
                break;
        }
        Debug.Log("personal=" + localScore + " : allyteam=" + AllyScore);
    }

    protected override Material setMaterial(PlayerState id)
    {
        if (id.Equals(PlayerState.DEAD))
        {
            return DEAD_MATERIAL;
        } else
        {
            return LIVE_MATERIAL;
        }
    }
    protected override void Shoot(string controllerNum)
    {
        float fire = Input.GetAxisRaw($"RightTrigger_P{controllerNum}");

        if (fire >= 0.3f && Time.time > NextFire && localScore > 0 && AllyScore > 0)
        {
            float startTime = Time.time;

            NextFire = Time.time + ( 1 / FireRate ); // 1/FireRate gives period of shots
            Debug.Log("FIRE!");
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().setOwner(this);
            setPlayerScore(-1);
        }
    }

    public override void setTeamScore(int score)
    {
        AllyScore += score;
    }
}
