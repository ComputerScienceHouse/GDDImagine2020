using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Manager : MonoBehaviour
{
    int score1;
    int score2;
    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.color = Color.black;
        GUI.backgroundColor = Color.grey;
        GUI.Box(new Rect(0, 0, 250, 80), "Player One's Score: " + score1);
        GUI.Box(new Rect(Screen.width-250, 0, 250, 80), "Player Two's Score: " + score2);
    }


    //Adds a certain number of points to the entered player
    public void AddScore(int player, int addition)
    {
        if (player == 1)
        {
            score1 += addition;
        }
        else if (player == 2)
        {
            score2 += addition;
        }
        else return;
    }
}
