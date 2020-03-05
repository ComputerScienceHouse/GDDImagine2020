using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        type = ModType.ReverseControls;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("REVERSE Start");
        if (player.speedMultiplier > 0)
        {
            player.speedMultiplier *= -1;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedMultiplier *= -1;
        Debug.Log("REVERSE End");
    }
}