﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GamePlayManager : MonoBehaviour {
    
    public static GamePlayManager Instance;

    public Action onGameStarted;
    public Action onGameOvered;

    private bool isGameOvered = false;
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    
    void Start()
    {
        Time.timeScale = 1f;
    }
    

    public void GameStart()
    { 
        onGameStarted?.Invoke();
    }
    

    public void GameOver()
    {
        isGameOvered = true;
        onGameOvered?.Invoke();
        StartCoroutine(GameOverCoroutine());
    }
    
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
    
        Time.timeScale = 0f;
        UIManager.Instance.ShowGameOverUI();
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOvered()
    {
        return isGameOvered;
    }
}
