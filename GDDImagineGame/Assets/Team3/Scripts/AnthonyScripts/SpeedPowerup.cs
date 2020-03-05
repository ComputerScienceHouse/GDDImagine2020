using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Collidable
{
    [Range(1, 5)]
    public float speedBoostMultiplier = 2;

    protected override void Start()
    {
        isPermanent = false;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        if (player.speedBoostMultiplier == 1)
        {
            player.speedBoostMultiplier = speedBoostMultiplier;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedBoostMultiplier = 1;
    }
}
