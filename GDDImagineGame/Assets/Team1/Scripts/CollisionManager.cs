using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public bool AABB;
    //public bool circle;
    // Start is called before the first frame update
    void Start()
    {
        //circle = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void ColorSprite(GameObject a, GameObject b, Color color)
    //{
    //    a.GetComponent<SpriteRenderer>().color = color;
    //    b.GetComponent<SpriteRenderer>().color = color;
    //}

    //Detects object collision via AABB format
    public bool AABBCollision(GameObject a, GameObject b)
    {
        Bounds aBounds = a.GetComponent<BoxCollider>().bounds;
        Bounds bBounds = b.GetComponent<BoxCollider>().bounds;
        if (aBounds.min.x < bBounds.max.x &&
            aBounds.max.x > bBounds.min.x &&
            aBounds.min.y < bBounds.max.y &&
            aBounds.max.y > bBounds.min.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public bool CircleCollision(GameObject a, GameObject b)
    //{
    //    Bounds aBounds = a.GetComponent<SpriteRenderer>().bounds;
    //    Bounds bBounds = b.GetComponent<SpriteRenderer>().bounds;
    //    float aRad = aBounds.size.x / 2;
    //    float bRad = bBounds.size.x / 2;
    //    Vector3 distance = aBounds.center - bBounds.center;
    //    if (distance.magnitude <= (aRad + bRad))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
