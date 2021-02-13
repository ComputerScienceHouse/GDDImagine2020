using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrPU : Powerup
{
    private static float duration = 3.0f;

    public SpeedIncrPU(GameObject gameObject, float duration) : base(gameObject, duration)
    {

    }

    protected void Activate(Player player)
    {
        base.Activate(player);
        player.setSpeed(player.getSpeed() * 1.5f);
    }

    protected void Deactivate(Player player) 
    {
        base.Deactivate(player);
        player.setSpeed(player.defaultSpeed);
    }
}
