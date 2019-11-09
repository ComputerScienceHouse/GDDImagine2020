using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	// Provides access to all four players
	public PlayerController[] players;

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

		// Increase timer
		if (timer < timeToChoose)
		{
			timer += Time.deltaTime;
			timerText.text = "Timer: " + Mathf.Round((timeToChoose - timer) * 100) / 100;
		}

		// Once timer has reached timeToChoose, handle choices
		else
		{
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
							target = (i - 1) % 4;
							break;
						case Choice.StealAcross:
							target = (i + 2) % 4;
							break;
						case Choice.StealRight:
							target = (i + 1) % 4;
							break;
					}

					//ignore if the chosen player is blocking, otherwise give half of score to player stealing
					if (choices[target] != Choice.Block)
					{
						int half = (int)Mathf.Ceil(players[target].score / 2);
						players[i].score += half;
						players[target].score -= half;
					}
				}
			}

			// Only one player went for the pot, give them pot value
			if (potNum == 1)
			{
				players[potPlayerNum].score += potValue;
			}

			//After resolving choices and updating scores, check to see if the game would end here
			//reset players choices and give them some pity money
			foreach (PlayerController pc in players)
			{
				pc.choice = Choice.None;
				pc.score += 1;
				Debug.Log(pc.name + "'s score is: " + pc.score);
			}

			// Increase round number
			roundNum++;

			// End game if round number is greater than number of rounds
			if (roundNum > numRounds)
			{
				GameOver();
				return;
			}

			// Increase pot value
			potValue += potIncrease;

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
