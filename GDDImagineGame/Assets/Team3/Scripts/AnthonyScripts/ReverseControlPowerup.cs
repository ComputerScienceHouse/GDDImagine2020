using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        player.speedMultiplier *= -1;
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedMultiplier *= -1;
    }
}