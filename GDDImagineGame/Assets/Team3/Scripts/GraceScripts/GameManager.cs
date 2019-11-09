using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public lists/variables
    public GameObject[] players;
    public List<GameObject> collidables;
    public GameObject cam;
    public Canvas can;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("player");
        collidables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 1);
        DisplayPoints();
    }

    // point management
    void AddPoints(GameObject player, int score)
    {
       // player.score += score;
    }

    void SubtractPoints(GameObject player, int score)
    {
       // player.score -= score;
    }

    void DisplayPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            

            if (can.gameObject.transform.GetChild(i).name == "Player1Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 1: 1";

            } else if (can.gameObject.transform.GetChild(i).name == "Player2Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 2: 1";
            } else if (can.gameObject.transform.GetChild(i).name == "Player3Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 3: 1";
            } else
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 4: 1";
            }
        }
    }
}
