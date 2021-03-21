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

    float camSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        collidables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + (camSpeed * Time.deltaTime));
        DisplayPoints();
    }

    // point management
    public void AddPoints(GameObject player, int score)
    {
       player.GetComponent<Movement>().Score += score;
    }

    public void SubtractPoints(GameObject player, int score)
    {
        player.GetComponent<Movement>().Score -= score;
    }

    void DisplayPoints()
    {
        for (int i = 0; i < players.Length; i++)
        {
            

            if (can.gameObject.transform.GetChild(i).name == "Player1Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 1: " + players[i].GetComponent<Movement>().Score;

            } else if (can.gameObject.transform.GetChild(i).name == "Player2Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 2: " + players[i].GetComponent<Movement>().Score;
            } else if (can.gameObject.transform.GetChild(i).name == "Player3Txt")
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 3: " + players[i].GetComponent<Movement>().Score;
            } else
            {
                can.gameObject.transform.GetChild(i).GetComponent<Text>().text = "Player 4: " + players[i].GetComponent<Movement>().Score;
            }
        }
    }
}
