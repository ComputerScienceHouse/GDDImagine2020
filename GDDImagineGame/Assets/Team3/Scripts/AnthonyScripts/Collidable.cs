using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    protected GameManager gameManager;

    public abstract void OnCollide(GameObject other);

    private void OnTriggerEnter(Collider other)
    {
        OnCollide(other.gameObject);
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
