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
        base.Start();
    }

    protected override void PlayerModFunc(Movement player)
    {
        player.speedMultiplier *= slowDebuffMultiplier;
    }

    protected override void PlayerModCallback(Movement player)
    {
        player.speedMultiplier /= slowDebuffMultiplier;
    }
}
