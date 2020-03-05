using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    // The time that the buff/debuff last for
    [Range(0.5f, 5)]
    public float timeLimit = 0.5f;

    // If the buff is permanent or not
    protected bool isPermanent;
    // The type the of the buff or debuff
    protected ModType type;

    /// <summary>
    /// The function called when a player collides with the collidable
    /// </summary>
    /// <param name="player">The player controller that collided with the object</param>
    protected abstract void PlayerModFunc(Movement player);

    /// <summary>
    /// The function called when the buff/debuff ends (Only if the buff/debuff is not permanent)
    /// </summary>
    /// <param name="player">The player controller that was affected by the buff/debuff</param>
    protected abstract void PlayerModCallback(Movement player);

    /// <summary>
    /// The function called when a player object collides with this object; Puts the mods into effect and then destroys the gameObject
    /// </summary>
    /// <param name="player">The player controller that collided with this object</param>
    public virtual void OnCollide(Movement player)
    {
        if (isPermanent)
        {
            player.ApplyPlayerMod(PlayerModFunc);
        }
        else
        {
            player.ApplyPlayerMod(PlayerModFunc, type, timeLimit, PlayerModCallback);
        }

        Destroy(this.gameObject);
    }

    /// <summary>
    /// A function that is called when any gameObject enters the trigger box; If the gameObject is a player then the player collide function is called
    /// </summary>
    /// <param name="other">The collider attached to the gameObject that has entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollide(other.gameObject.GetComponent<Movement>());
        }
    }

    protected virtual void Start()
    {
        
    }

    private void Update()
    {
        if(transform.position.z - Camera.main.transform.position.z <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
