using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;

    public int ScoreVal { get { return score; } set { score = value; } }

    public static void setPlayerScore(GameObject gameObject, int score)
    {
        if (gameObject.tag == "Player")
        {
            Player.playerScore += score;
        }
        else if (gameObject.tag == "Enemy")
        {
            Player.enemyScore += score;
        }
        gameObject.GetComponent<Player>().localScore += score; // Increments player's personal score
    }
}
