using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Victor : MonoBehaviour
{
    [Header("Buttons Info")]
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button endBtn;

    [Header("Score Rec Info")]
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    [SerializeField] private UI_Score ui_Score;

    private void Awake()
    {
        continueBtn.onClick.AddListener(OnContinueBtnClicked);
        endBtn.onClick.AddListener(OnEndBtnClicked);
    }

    private void OnContinueBtnClicked()
    {
        // 恢复游戏时间
        Time.timeScale = 1f;
        SceneLoaderManager.instance.LoadNextScene();

        ui_Score.ResetLevelScore();

        this.gameObject.SetActive(false);
    }

    private void OnEndBtnClicked()
    {
        // 恢复游戏时间
        Time.timeScale = 1f;
        SceneLoaderManager.instance.LoadMenuScene();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // 显示分数
        if (ui_Score != null)
        {
            // 先更新再显示
            ui_Score.RefreshTotoalScoreAfterOneLevel();

            levelScoreText.text = ui_Score.scoreInThisLevel.ToString();
            totalScoreText.text = UI_Score.totoalScore.ToString();
        }
        else
        {
            Debug.LogWarning("UI_Score instance is null.");
            levelScoreText.text = "0";
            totalScoreText.text = "0";
        }
        // 暂停游戏时间
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnContinueBtnClicked();
        }
    }
}
