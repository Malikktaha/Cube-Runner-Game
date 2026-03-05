using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public Text finalScoreText;

    int myScore = 0;
    
    void Update()
    {
        ScoreText.text = myScore.ToString();
        finalScoreText.text = "Score: "+ myScore.ToString();
    }
    public void Addscore(int score)
    {
        myScore = myScore + score;
    }
}
