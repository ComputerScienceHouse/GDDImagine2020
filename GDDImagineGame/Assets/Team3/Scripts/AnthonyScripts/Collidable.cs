using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    protected GameManager gameManager;

    [Range(0.5f, 5)]
    public float timeLimit = 0.5f;

    protected bool isPermanent;

    protected abstract void PlayerModFunc(Movement player);
    protected abstract void PlayerModCallback(Movement player);

    public void OnCollide(Movement player)
    {
        if (isPermanent)
        {
            player.ApplyPlayerMod(PlayerModFunc);
        }
        else
        {
            player.ApplyPlayerMod(PlayerModFunc, timeLimit, PlayerModCallback);
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollide(other.gameObject.GetComponent<Movement>());
        }
    }

    protected virtual void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(transform.position.z - Camera.main.transform.position.z <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
