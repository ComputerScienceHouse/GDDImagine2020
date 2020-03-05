using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinsPowerup : Collidable
{
    protected override void Start()
    {
        isPermanent = false;
        type = ModType.DoubleCoin;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        Debug.Log("2X Start");
        if (player.coinMultiplier < 2)
        {
            player.coinMultiplier *= 2;
        }
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.coinMultiplier /= 2;
        Debug.Log("2X End");
    }
}
