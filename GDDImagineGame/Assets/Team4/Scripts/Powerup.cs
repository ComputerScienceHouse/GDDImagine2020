using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    private float maxDuration;
    private float timeLeft;

    private GameObject gameObject;

    private Player activePlayer; // this is the player being effected by the powerup

    public Powerup(GameObject gameObject, float maxDuration) 
    {
        this.maxDuration = maxDuration;
        timeLeft = maxDuration;
        activePlayer = null;
        this.gameObject = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activePlayer != null) {
            if (timeLeft > 0.0f) timeLeft -= Time.deltaTime;
            else
            {
                Deactivate(activePlayer);
            }
        }
    }

    protected virtual void Activate(Player player) {
        player.setHasPowerup(true);
        activePlayer = player;
    }

    protected virtual void Deactivate(Player player) {
        player.setHasPowerup(false);
        activePlayer = null;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && !collider.gameObject.GetComponent<Player>().getHasPowerup()) {
            // want to apply effect to player
            Activate(collider.gameObject.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}
