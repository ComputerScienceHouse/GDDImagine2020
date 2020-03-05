using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinsPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        if(player.coinMultiplier < 2)
        {
            player.coinMultiplier *= 2;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.coinMultiplier /= 2;
    }
}
