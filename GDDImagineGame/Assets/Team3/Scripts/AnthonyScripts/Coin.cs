using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collidable
{
    [Range(1, 100)]
    public int scoreValue = 1;

    protected override void Start()
    {
        isPermanent = true;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        player.Score += scoreValue * player.coinMultiplier;
    }

    protected override void PlayerModCallback(Movement player) {}
}
