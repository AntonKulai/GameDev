using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    [Header(" Elements ")]
    [SerializeField] private GameObject rocket;
    
    [Header(" Settings ")]
    private int currentScore = 0;

    [Header(" Actions ")]
    public static Action<int> onCurrentScoreUpdated;
    public static Action<int> onBestScoreUpdated;
    
    void Update()
    {
        int positionY = (int) rocket.transform.position.y / 2;
        if (positionY > currentScore)
        {
            currentScore = positionY;
            onCurrentScoreUpdated?.Invoke(currentScore);
        }
        

        if (currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            onBestScoreUpdated?.Invoke(currentScore);
        }
    }
    

    
    

}
