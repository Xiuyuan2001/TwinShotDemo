using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public Player player;
    public GameObject ui_failure;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if(player.playerStats.currentHealth <= 0)
        {
            ui_failure.SetActive(true);
        }
    }
}
