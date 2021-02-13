using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReducPU : Powerup
{
    private static float duration = 3.0f;

    public SpeedReducPU(GameObject gameObject, float duration) : base(gameObject, duration) { 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Activate(Player player)
    {
        base.Activate(player);
        player.setSpeed(player.getSpeed() * 0.75f);
    }

    protected void Deactivate(Player player)
    {
        base.Deactivate(player);
        player.setSpeed(player.defaultSpeed);
    }
}
