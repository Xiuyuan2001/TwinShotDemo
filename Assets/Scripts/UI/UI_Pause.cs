using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    public static bool isPause = false;

    public GameObject OnPauseUI;

    public void PauseGame()
    {
        if(!isPause)
        {
            Time.timeScale = 0f;
            isPause = true;
            OnPauseUI.SetActive(true);
            PlayerManager.instance.player.anim.speed = 0;
        }
    }

    public void ResumeGame()
    {
        if (isPause)
        {
            Time.timeScale = 1f;        
            isPause = false;
            OnPauseUI.SetActive(false);
            PlayerManager.instance.player.anim.speed = 1;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        this.gameObject.SetActive(false); PlayerManager.instance.player.anim.speed = 1;

        SceneLoaderManager.instance.LoadMenuScene();
    }

    private void Update()
    {
        if(isPause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResumeGame();
                Input.ResetInputAxes();
            }
        }
    }
}
