using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private Image healbarImg;

    [SerializeField]
    private HealthSO playerHealthData;

    private float MaxValue { get; set; }
    private float currentValue;
    private void OnEnable()
    {
        
        
        PlayerStats.OnPlayerTakeDamage+= OnPlayerTakeDamage;
    }

    public void SetheathBar()
    {
        healbarImg.fillAmount = playerHealthData.currentHP / playerHealthData.maxHP;
    }

    private void OnPlayerTakeDamage(float damage)
    {
  
        SetheathBar();
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }
}
