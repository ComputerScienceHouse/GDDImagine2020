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
        type = ModType.SpeedUp;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("SPEED Start");
        if (player.speedBoostMultiplier == 1)
        {
            player.speedBoostMultiplier = speedBoostMultiplier;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedBoostMultiplier = 1;
        Debug.Log("SPEED End");
    }
}
