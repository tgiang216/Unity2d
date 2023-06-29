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

    [SerializeField]
    private Transform thunderIcon;
    [SerializeField]
    private Transform fireIcon;
    [SerializeField]
    private Transform iceIcon;
    [SerializeField]
    private Transform selectIcon;
    private void OnEnable()
    {
        
        
        PlayerStats.OnPlayerTakeDamage += OnPlayerTakeDamage;
        PlayerCombatCtrl.OnChangeWeapon += OnPlayerChangeWeapon;
    }

    public void SetheathBar()
    {
        healbarImg.fillAmount = playerHealthData.currentHP / playerHealthData.maxHP;
    }

    

    private void OnDisable()
    {
        PlayerStats.OnPlayerTakeDamage -= OnPlayerTakeDamage;
        PlayerCombatCtrl.OnChangeWeapon -= OnPlayerChangeWeapon;
    }

    private void OnPlayerTakeDamage(float damage)
    {

        SetheathBar();
    }
    private void OnPlayerChangeWeapon(int type)
    {
        selectIcon.gameObject.SetActive(true);
        switch(type)
        {
            case 0:
                selectIcon.gameObject.SetActive(false);
                break;
            case 1:
                selectIcon.position = thunderIcon.position;
                break;
            case 2:
                selectIcon.position = fireIcon.position;
                break;
            case 3:
                selectIcon.position = iceIcon.position;
                break;
        }
    }
}
