using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manager : MonoBehaviour
{

    //private var for keeping track of the time
    public float timer = 0.0f;

    //vars for updating the timer UI
    //public TextMeshProUGUI timerText;
    //public GameObject textObject;

    //public var for how long a "round" should be (time when players can choose an action)
    public float timeToChoose;
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

    //var for checking if players are animating after a round has been calculated
    private bool playersAnimating;

    private PlayerController[] players;

    // Start is called before the first frame update
    void Start()
    {
        //initialize some vars
        players = new PlayerController[] { player1.GetComponent<PlayerController>(), player2.GetComponent<PlayerController>(), player3.GetComponent<PlayerController>(), player4.GetComponent<PlayerController>() };
        roundNum = 1;
        gameOver = false;
        playersAnimating = false;
        //timerText = textObject.GetComponent<TextMeshProUGUI>();
        //timerText.SetText("Timer: " + timeToChoose);
    }

    // Update is called once per frame
    void Update()
    {
        ////make sure animations aren't happening at the moment
        //if (!players[0].animating && !players[1].animating && !players[2].animating && !players[3].animating)
        //{
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
                else if (choices[i] == Choice.stealLeft || choices[i] == Choice.stealAcross || choices[i] == Choice.stealRight)
                {
                    int target = -1;
                    float degreesToRotate = 0;
                    switch (choices[i])
                    {
                        case Choice.stealLeft:
                            target = (i - 1) % 4;
                            degreesToRotate = -90;
                            break;
                        case Choice.stealAcross:
                            target = (i + 2) % 4;
                            degreesToRotate = 180;
                            break;
                        case Choice.stealRight:
                            target = (i + 1) % 4;
                            degreesToRotate = 90;
                            break;
                        default:
                            break;
                    }
                    //ignore if the chosen player is blocking, otherwise give half of score to player stealing
                    if (choices[target] != Choice.block)
                    {
                        int half = (int)Mathf.Ceil(players[target].score / 2);
                        //players[i].PlayerMoveToStealSuccessFul(degreesToRotate);
                        players[i].score += half;
                        players[target].score -= half;
                    }
                    else
                    {
                        //players[i].PlayerMoveToStealUnsuccessFul(degreesToRotate);
                    }
                }
            }
            //if only 1 going for pot, give them pot value
            if (potNum == 1)
            {
                //players[potPlayerNum].PlayerMoveToPotSuccessFul();
                //players[potPlayerNum].animating = true;
                players[potPlayerNum].score += potValue;
            }
            else if (potNum > 1)
            {
                foreach (PlayerController pc in players)
                {
                    if (pc.choice == Choice.pot)
                    {
                        //pc.PlayerMoveToPotUnsuccessFul();
                    }
                }
            }

            foreach (PlayerController pc in players)
            {
                if (pc.choice == Choice.block)
                {
                    //pc.PlayerMoveToBlock();
                }
            }

            //After resolving choices and updating scores, check to see if the game would end here
            //reset players choices and give them some pity money
            foreach (PlayerController pc in players)
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
            if (roundNum >= numRounds)
            {
                int highScorePlayer = -1;
                int highScore = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].score > highScore)
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
            //timerText.SetText("Timer: " + Mathf.Round((timeToChoose - timer)*100)/100);
        }
        //}
    }
}
