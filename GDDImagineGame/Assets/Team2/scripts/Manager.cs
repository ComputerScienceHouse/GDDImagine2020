using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Provides access to all four players
    public PlayerController[] players;

    // Provides access to the coin piles of the four players
    public GameObject[] coinPiles;

    // Provides access to the main "pot" (dragon horde, w/e)
    public GameObject pot;

    // The Text object that displays the time remaining in the round
    public Text timerText;

    // Duration in seconds of one game round
    public float timeToChoose;

    // The current value of the pot
    public int potValue;

    // The amount by which the pot increases value after each round
    public int potIncrease;

    // The total number of rounds in the game
    public int numRounds;

    // The current round number
    private int roundNum;

    // If the game is over, prevent Update() from happening
    private bool gameOver;

    // Keeps track of elapsed game time
    private float timer;

    // Amount to scale a pile by
    private float pileScale;

    void Start()
    {
        timer = 0;
        roundNum = 1;
        gameOver = false;
    }

    void Update()
    {
        // Prevent Update() when the game has ended
        if (gameOver) return;

        // Animate players if needed
        for (int i = 0; i < players.Length; i++)
        {
            // Only animate if this player has an active animation enum
            if (players[i].anim != Anim.None)
            {
                players[i].PlayerAnimation();
                return;
            }
        }

        // Increase timer when there's still time to choose
        if (timer < timeToChoose)
        {
            timer += Time.deltaTime;
            timerText.text = "Timer: " + Mathf.Round((timeToChoose - timer) * 100) / 100;
        }

        // Once timer has reached timeToChoose, handle choices and animate accordingly
        else
        {
            Debug.Log("Beginning to process choices");
            // Collect Player choices
            Choice[] choices = { players[0].choice, players[1].choice, players[2].choice, players[3].choice };

            // Number of players who went for the pot
            int potNum = 0;

            // ID of player who went for the pot
            int potPlayerNum = -1;

            // Handle each choice
            for (int i = 0; i < choices.Length; i++)
            {
                // Player went for the pot
                if (choices[i] == Choice.Pot)
                {
                    potNum++;
                    potPlayerNum = i;
                }

                // Player chose to steal
                else if (choices[i] == Choice.StealLeft || choices[i] == Choice.StealAcross || choices[i] == Choice.StealRight)
                {
                    int target = -1;

                    switch (choices[i])
                    {
                        case Choice.StealLeft:
                            target = ((i - 1) % 4 + 4) % 4;
                            break;
                        case Choice.StealAcross:
                            target = ((i + 2) % 4 + 4) % 4;
                            break;
                        case Choice.StealRight:
                            target = ((i + 1) % 4 + 4) % 4;
                            break;
                    }

                    // set the destination position for this player to slerp to
                    players[i].targetPos = coinPiles[target].transform.position;

                    // If target player blocked, no change in score occurs between the two of them
                    if (choices[target] == Choice.Block)
                    {
                        players[i].anim = Anim.UnsuccessfulSteal;
                        players[i].startTime = Time.time;
                        players[target].anim = Anim.Block;
                        players[target].startTime = Time.time;
                    }

                    // If target is not blocked, give half of their score to player stealing
                    else
                    {
                        players[i].anim = Anim.SuccessfulSteal;
                        players[i].startTime = Time.time;
                        int half = (int)Mathf.Ceil(players[target].score / 2);
                        players[i].score += half;
                        players[target].score -= half;
                    }
                }
            }

            // Only one player went for the pot, give them pot value
            if (potNum == 1)
            {
                players[potPlayerNum].anim = Anim.SuccessfulPot;
                players[potPlayerNum].startTime = Time.time;
                players[potPlayerNum].score += potValue;
            }

            // More than one player went for the pot
            else if (potNum > 1)
            {
                foreach (PlayerController pc in players)
                {
                    if (pc.choice == Choice.Pot)
                    {
                        pc.anim = Anim.UnsuccessfulPot;
                        pc.startTime = Time.time;
                    }
                }
            }

            // Reset players choices, give them some pity money and update their coinpile prefabs accordingly
            for (int i = 0; i < players.Length; i++)
            {
                players[i].choice = Choice.None;
                players[i].score += 1;
                pileScale = (float)(players[i].score * 0.1);
                coinPiles[i].transform.localScale = new Vector3(pileScale, pileScale, pileScale);
                Debug.Log(players[i].name + "'s score is: " + players[i].score);
            }

            // Increase round number
            roundNum++;

            // End game if round number is greater than number of rounds
            if (roundNum > numRounds)
            {
                GameOver();
                return;
            }

            // Increase pot value and size
            potValue += potIncrease;
            pileScale = (float)(potIncrease * 0.1);
            pot.transform.localScale += new Vector3(pileScale, pileScale, pileScale);

            // Reset timer
            timer = 0.0f;
        }
    }

    ///
    /// When the game has ended, show results
    ///
    private void GameOver()
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

        timerText.text = "Game Over!";
        Debug.Log("The winner is player " + (highScorePlayer + 1) + " with a score of " + highScore);
        gameOver = true;
    }
}
