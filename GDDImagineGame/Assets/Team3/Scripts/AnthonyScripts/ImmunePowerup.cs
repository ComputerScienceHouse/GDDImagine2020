using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunePowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        player.isInvincible = true;
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.isInvincible = false;
    }
}
