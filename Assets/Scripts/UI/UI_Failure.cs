using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Failure : MonoBehaviour
{
    [Header("Buttons Info")]
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button endBtn;

    [Header("Score Rec Info")]
    [SerializeField] private TextMeshProUGUI totalScoreText;

    [SerializeField] private UI_Score ui_Score;

    private void Awake()
    {
        restartBtn.onClick.AddListener(OnRestartBtnClicked);
        endBtn.onClick.AddListener(OnEndBtnClicked);
    }

    private void OnRestartBtnClicked()
    {
        // 恢复游戏时间
        Time.timeScale = 1f;

        PlayerManager.instance.player.playerStats.ResetStats();
        SceneLoaderManager.instance.ReloadCurrentScene();

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

            totalScoreText.text = UI_Score.totoalScore.ToString();
        }
        else
        {
            Debug.LogWarning("UI_Score instance is null.");
            totalScoreText.text = "0";
        }

        StartCoroutine(Delay());

    }

    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnRestartBtnClicked();
        }
    }
}
