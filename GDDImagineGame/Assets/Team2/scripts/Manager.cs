using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public enum GameState
{
    RoundTransition,
    PlayerSelection,
    AnimatePlayers
}
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

    //The text object that displays the round number and a boolean that determines if it is transitioning in or out
    public Text roundText;
    private bool transitionIn;

    //Integer that tracks the current round number
    private int roundNum;

    //Create a list to hold all of the player score text objects
    public List<Text> playerScores;

    // Duration in seconds of one game round
    public float timeToChoose;

    // The current value of the pot
    [SerializeField] private int potValue;

    // The amount by which the pot increases value after each round
    [SerializeField] private int potIncrease;

    // The total number of rounds in the game
    public int numRounds;

    // The current round number
    private int currentRound;

    // If the game is over, prevent Update() from happening
    private bool gameOver;

    // Keeps track of elapsed game time
    private float timer;

    // Amount to scale a pile by
    private float pileScale;

    //Variable to hold the game state
    private GameState gameState;


    //PROPERTIES
    public float Timer
    {
        get
        {
            return timer;
        }
    }

    void Start()
    {
        timer = 0;
        currentRound = 1;
        gameOver = false;
        gameState = GameState.RoundTransition;
        transitionIn = false;
        roundNum = 1;
    }

    void Update()
    {
        // Prevent Update() when the game has ended
        if (gameOver) return;

        //Determine the current game state
        switch (gameState)
        {
            case GameState.RoundTransition:
                {
                    if (transitionIn)
                    {
                        float alpha;
                        alpha = roundText.color.a + .5f * Time.deltaTime;
                        roundText.color = new Vector4(roundText.color.r, roundText.color.g, roundText.color.b, alpha);
                        if (alpha >= 1)
                        {
                            alpha = 1;
                            transitionIn = false;
                        }
                    }
                    else
                    {
                        float alpha;
                        alpha = roundText.color.a - .5f * Time.deltaTime;
                        roundText.color = new Vector4(roundText.color.r, roundText.color.g, roundText.color.b, alpha);

                        if (alpha <= 0)
                        {
                            alpha = 0;
                            transitionIn = true;
                            roundNum++;
                            roundText.text = "Round " + roundNum;
                            gameState = GameState.PlayerSelection;
                        }
                    }
                    break;
                }

            case GameState.PlayerSelection:
                {
                    // Increase timer
                    if (timer < timeToChoose)
                    {
                        timer += Time.deltaTime;
                        timerText.text = "Timer: " + (int)Mathf.Round((timeToChoose - timer) * 100) / 100;
                    }

                    // Once timer has reached timeToChoose, handle choices
                    else
                    {
                        // Collect Player choices
                        Choice[] choices = { players[0].Choice, players[1].Choice, players[2].Choice, players[3].Choice };

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
                            else if (choices[i] == Choice.StealLeft || choices[i] == Choice.StealRight)
                            {
                                //Index of object being targeted
                                int target = -1;

                                switch (choices[i])
                                {
                                    case Choice.StealLeft:
                                        if (i != 3)
                                        {
                                            target = i + 1;
                                        }
                                        else
                                        {
                                            target = 0;
                                        }
                                        break;
                                    case Choice.StealRight:
                                        if (i != 0)
                                        {
                                            target = i - 1;
                                        }
                                        else
                                        {
                                            target = choices.Length - 1;
                                        }
                                        break;
                                }
                                // Ignore if the chosen player is blocking, otherwise give half of score to player stealing
                                if (choices[target] != Choice.Block)
                                {
                                    int half = (int)Mathf.Ceil(players[target].Score / 2);
                                    players[i].Score += half;
                                    players[target].Score -= half;
                                    pileScale = (float)(half * 0.1);
                                    coinPiles[i].transform.localScale += new Vector3(pileScale, pileScale, pileScale);
                                    coinPiles[target].transform.localScale -= new Vector3(pileScale, pileScale, pileScale);
                                }
                            }
                        }

                        // Only one player went for the pot, give them pot value
                        if (potNum == 1)
                        {
                            players[potPlayerNum].Score += potValue;
                            pileScale = (float)(potValue * 0.1);
                            coinPiles[potPlayerNum].transform.localScale += new Vector3(pileScale, pileScale, pileScale);
                        }

                        // Reset players choices and give them some pity money
                        for (int i = 0; i < players.Length; i++)
                        {
                            players[i].Choice = Choice.None;
                            players[i].Score += 1;
                            coinPiles[i].transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                        }

                        // Increase round number
                        currentRound++;

                        // End game if round number is greater than number of rounds
                        if (currentRound > numRounds)
                        {
                            GameOver();
                            return;
                        }

                        // Increase pot value and size
                        potValue += potIncrease;
                        pileScale = (float)(potIncrease * 0.1);
                        pot.transform.localScale += new Vector3(pileScale, pileScale, pileScale);

                        UpdateScores();

                        // Reset timer
                        timer = 0.0f;
                        timerText.text = "Timer: " + timeToChoose;
                        gameState = GameState.RoundTransition;
                    }
                    break;
                }

            case GameState.AnimatePlayers:
                {
                    break;
                }
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
            if (players[i].Score > highScore)
            {
                highScore = players[i].Score;
                highScorePlayer = i;
            }
        }

        timerText.text = "Game Over!";
        Debug.Log("The winner is player " + (highScorePlayer + 1) + " with a score of " + highScore);
        gameOver = true;
    }

    private void UpdateScores()
    {
        for (int i = 0; i < players.Length; i++)
        {
            playerScores[i].text = "Player " + (i + 1) + ": " + players[i].Score;
        }
    }
}
