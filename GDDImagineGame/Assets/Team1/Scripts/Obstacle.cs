using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isBouncy;
    public bool isBreakable;
    public bool isSolid;
    public int health;
    public float bouncePower;
    // Start is called before the first frame update
    void Start()
    {
        isBouncy = false;
        isBreakable = false;
        isSolid = true;
        health = -1;
        bouncePower = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Bounce(GameObject obj)
    {
        //dart.velocity * bouncePower in opposite direction of collision
    }

    void Collide(GameObject obj)
    {
        //Keep these three separate so we can invoke multiple at once, like a 
        //breakable bouncy block
        if (isBreakable)
        {
            health--;
            if(health <= 0) {
                Destroy(gameObject, 0f);
            }
        }

        if (isBouncy)
        {
            Bounce(obj);
        }

        if(isSolid){
            //Destroy obj, will most likely need to be handled by agentmanager
        }
    }
}
