using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        player.maxJumpVelocity /= 100000000;
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.maxJumpVelocity *= 100000000;
    }
}