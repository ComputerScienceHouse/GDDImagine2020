using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject balloon;
    public GameObject obstacle;

    /// <summary>
    /// Number of frames in between the spawning of balloon waves
    /// </summary>
    public int spawnRate;
    //Random rand;
    List<GameObject> balloonList;
    List<GameObject> obstacleList;
    Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {   
        spawnPoint = new Vector3(-10, 100, 0);
        balloonList = new List<GameObject>();
        obstacleList = new List<GameObject>();
        //rand = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures balloons are spawned every 100 Frames
        if (Time.frameCount % spawnRate == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (spawnPoint.x > 10)
                {
                    spawnPoint = new Vector3(-10, 20, 0);
                }
                balloonList.Add(GameObject.Instantiate(balloon, spawnPoint, Quaternion.identity));
                spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
            }
        }

        // Randomly also spawns obstacles
        // All the code I added in last commit is probably bugged but we're pushing during a meeting yeet
        float obstChance = Random.Range(0.00f, 10.00f);
        if(obstChance < 0.15f){
            if (spawnPoint.x > 10)
            {
                spawnPoint = new Vector3(-10, 20, 0);
            }
            obstacleList.Add(GameObject.Instantiate(obstacle, spawnPoint, Quaternion.identity));
            spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
        }
    }
}
