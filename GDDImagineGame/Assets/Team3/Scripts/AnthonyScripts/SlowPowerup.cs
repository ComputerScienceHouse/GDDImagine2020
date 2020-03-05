using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPowerup : Collidable
{
    [Range(0.05f, 1)]
    public float slowDebuffMultiplier = 0.5f;

    protected override void Start()
    {
        isPermanent = false;
        type = ModType.SlowDown;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("SLOW Start");
        if (player.speedBoostMultiplier == 1)
        {
            player.speedBoostMultiplier = slowDebuffMultiplier;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedBoostMultiplier = 1;
        Debug.Log("SLOW End");
    }
}
