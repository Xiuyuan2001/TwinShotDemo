using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Score : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] public TextMeshProUGUI scoreText;           

    [Header("Score Values")]
    [SerializeField] private int goldCoinValue = 100;  
    [SerializeField] private int silverCoinValue = 50; 

    public static int totoalScore = 0;
    //private int currentScore = 0;
    public int scoreInThisLevel = 0;

    public static UI_Score instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();

        // 初始化分数显示
        RefreshTotoalScoreAfterOneLevel();
        // 更新分数
        UpdateScoreDisplay(scoreInThisLevel);

    }

    private void Update()
    {
        UpdateScoreDisplay(scoreInThisLevel);
    }

    // 添加分数的主要方法
    public void AddScore(int points)
    {
        scoreInThisLevel += points;
    }

    // 添加金币分数
    public void AddGoldCoin()
    {
        AddScore(goldCoinValue);

    }

    // 添加银币分数
    public void AddSilverCoin()
    {
        AddScore(silverCoinValue);

    }

    // 更新总分数
    public void RefreshTotoalScoreAfterOneLevel()
    {
        totoalScore += scoreInThisLevel;
        //scoreInThisLevel = 0;
    }

    public void ResetLevelScore()
    {
        scoreInThisLevel = 0;
        UpdateScoreDisplay(scoreInThisLevel);
    }

    // 更新分数显示
    private void UpdateScoreDisplay(int score)
    {
        if (scoreText != null)
        {
            string scoreString = score.ToString();
            scoreText.text = scoreString;
        }
    }
}
