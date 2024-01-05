using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public static UIManager Instance;


    [SerializeField] private Text tabToStartText;

    [Header(" Score ")]
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text bestScoreText;
    
    [Header(" GameOver ")]
    [SerializeField] private GameObject gameOverPanel;


    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    
    private void Start()
    {
        GamePlayManager.Instance.onGameStarted += GamePlayManager_OnGameStarted;
        GamePlayManager.Instance.onGameOvered+= GamePlayManager_OnGameOvered;

        ScoreManager.onCurrentScoreUpdated += ScoreManager_OnCurrentScoreUpdated;
        ScoreManager.onBestScoreUpdated += ScoreManager_onBestScoreUpdated;
        
        currentScoreText.text = "0";
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        
        tabToStartText.gameObject.SetActive(true);
    }

    
    private void OnDestroy()
    {
        GamePlayManager.Instance.onGameStarted -= GamePlayManager_OnGameStarted;
        GamePlayManager.Instance.onGameOvered -= GamePlayManager_OnGameOvered;

        ScoreManager.onCurrentScoreUpdated -= ScoreManager_OnCurrentScoreUpdated;
        ScoreManager.onBestScoreUpdated -= ScoreManager_onBestScoreUpdated;
    }

    
    private void GamePlayManager_OnGameStarted()
    {
        tabToStartText.gameObject.SetActive(false);
    }
    
    
    private void GamePlayManager_OnGameOvered()
    {

    }
    
    
    private void ScoreManager_OnCurrentScoreUpdated(int score)
    {
        currentScoreText.text = score.ToString();
    }

    
    private void ScoreManager_onBestScoreUpdated(int score)
    {
        bestScoreText.text = score.ToString();
    }
    

    public void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        ChangeScoreTextColor(Color.white);
    }

    
    private void ChangeScoreTextColor(Color color)
    {
        currentScoreText.color = color;
        bestScoreText.color = color;
    }


}