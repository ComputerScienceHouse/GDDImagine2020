using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
enum ObstacleType
{
    Jumpable,
    Nonjumpable,
}
*/
public class Obstaclemanager : MonoBehaviour
{
    public GameObject obstacle;
    //ObstacleType whichObstacle;
    // Start is called before the first frame update
    void Start()
    {
        //Randomly switches between two possible forms of physical obstacle
        int size = Random.Range(1, 3);
        switch (size)
        {
            case 1:
                obstacle.transform.localScale = new Vector3(15, 1, 1); //fence
                break;
            case 2:
                obstacle.transform.localScale = new Vector3(2, 18, 2); //tree
                break;
        }
    }
}
