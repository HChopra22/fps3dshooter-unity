using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Game Controller that handles the score
public class GameController : MonoBehaviour
{
    [Header("Score Varibles")]
    float currentScore;
    public Text scoreAmount;

    //currently the score is 0 and make sure the UI prints this
    private void Start()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    //when score is being added, change the current score to the updated ammount
    //update the UI with the new amount
    public void AddScore(float amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    //the UI prints the score as text on the HUD
    private void UpdateScoreUI()
    {
        scoreAmount.text = currentScore.ToString("0");
    }
}
