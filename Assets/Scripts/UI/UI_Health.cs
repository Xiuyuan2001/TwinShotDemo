using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        slider = GetComponent<Slider>();
        playerStats = PlayerManager.instance.player.playerStats;
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.health;
        slider.value = playerStats.currentHealth;
    }
}
