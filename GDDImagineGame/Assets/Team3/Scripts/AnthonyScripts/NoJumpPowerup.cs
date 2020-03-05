using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        type = ModType.NoJump;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("JUMPN'T Start");
        if (player.maxJumpVelocity > 1)
        {
            player.maxJumpVelocity /= 100000000;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.maxJumpVelocity *= 100000000;
        Debug.Log("JUMPN'T End");
    }
}