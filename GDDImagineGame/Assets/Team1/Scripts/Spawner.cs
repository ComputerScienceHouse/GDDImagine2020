using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject balloon;
    List<GameObject> balloonList;
    Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(-10, 100, 0);
        balloonList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    { 
        
        for (int i = 0; i < 10; i++)
        {
            if (spawnPoint.x > 10)
            {
                spawnPoint = new Vector3(-10, 10, 0);
            }
            balloonList.Add(GameObject.Instantiate(balloon, spawnPoint, Quaternion.identity));
            spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
        }
    }
}
