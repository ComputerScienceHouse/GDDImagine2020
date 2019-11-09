using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    //private var for keeping track of the time
    private float timer = 0.0f;
    //public var for how long a "round" should be (time when players can choose an action)
    public float timeToChoose = 0.0f;
    //public vars for managing the 4 players
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    //public var for the pot value
    public int potValue;
    public int potIncrease;

    //vars for rounds
    public int numRounds;
    private int roundNum;
    private bool gameOver;

    private PlayerController [] players;

    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerController[] { player1.GetComponent<PlayerController>(), player2.GetComponent<PlayerController>(), player3.GetComponent<PlayerController>(), player4.GetComponent<PlayerController>() };
        roundNum = 1;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if time is up for picking an action
        if (timer >= timeToChoose && !gameOver)
        {
            //reset timer
            timer = 0.0f;
            //Resolve player choices here

            //condense players choices for checks
            Choice[] choices = { players[0].choice, players[1].choice, players[2].choice, players[3].choice };

            //temp vars for checking who is going to pot
            int potNum = 0;
            int potPlayerNum = -1;

            //check players choices
            for (int i = 0; i < choices.Length; i++)
            {
                //if pot, increase pot num and assign player
                if (choices[i] == Choice.pot)
                {
                    potNum++;
                    potPlayerNum = i;
                }
                //handle steals
                else if (choices[i] == Choice.steal)
                {
                    //ignore if the next player over is blocking, otherwise give half of score to player stealing
                    if(choices[(i + 1) % 4] != Choice.block)
                    {
                        int half = (int)Mathf.Ceil(players[(i + 1) % 4].score / 2);
                        players[i].score += half;
                        players[(i + 1) % 4].score -= half;
                    }
                }
            }
            //if only 1 going for pot, give them pot value
            if(potNum == 1)
            {
                players[potPlayerNum].score += potValue;
            }

            //After resolving choices and updating scores, check to see if the game would end here
            //reset players choices and give them some pity money
            foreach(PlayerController pc in players)
            {
                pc.choice = Choice.none;
                pc.score += 1;
                Debug.Log(pc.name + "'s score is: " + pc.score);
            }
            //increase pots value
            potValue += potIncrease;

            //increase round number
            roundNum++;

            //ends the game if max rounds reached
            if(roundNum >= numRounds)
            {
                int highScorePlayer = -1;
                int highScore = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    if(players[i].score > highScore)
                    {
                        highScore = players[i].score;
                        highScorePlayer = i;
                    }
                }

                Debug.Log("The winner is player " + (highScorePlayer + 1) + " with a score of " + highScore);
                gameOver = true;
            }
        }
        else //default case, increment
        {
            timer += Time.deltaTime;
            /*
             Code for listening to player input and preventing them from making another choice would go here
             Use public game objects that can be set to specific object instances in the unity editor
             to set the variables that would be checked here
             */
        }
    }
}
