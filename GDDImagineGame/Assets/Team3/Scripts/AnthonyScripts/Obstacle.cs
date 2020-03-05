using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : Collidable
{
    [Range(1, 100)]
    public int obstacleValue = 1;

    protected override void Start()
    {
        isPermanent = true;
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        if (!player.isInvincible)
        {
            player.Score -= obstacleValue;
        }
    }

    protected override void PlayerModCallback(Movement player) { }
}
