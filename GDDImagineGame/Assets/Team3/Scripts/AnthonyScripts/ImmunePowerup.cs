using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunePowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        type = ModType.Immune;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("IMMUNE Start");
        player.isInvincible = true;
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.isInvincible = false;
        Debug.Log("IMMUNE End");
    }
}
