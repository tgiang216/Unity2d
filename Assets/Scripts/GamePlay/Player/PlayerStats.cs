using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //[SerializeField]
    //public float MaxHP { get; private set; }
    //[SerializeField]
    //private float currentHP;

    [SerializeField]
    private HealthSO healthData;

    [SerializeField]
    private PlayerStatesCtrl sm;
    [SerializeField]
    private PlayerCombatCtrl player;

    //public delegate void OnPlayerDie();
    public static event Action OnPlayerDie;
  
    public static event Action<float> OnPlayerTakeDamage;

    private void Awake()
    {
        sm = GetComponent<PlayerStatesCtrl>();
        player = GetComponent<PlayerCombatCtrl>();
    }
    private void Start()
    {
        healthData.currentHP = healthData.maxHP;
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Die();
        }
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item == null) Debug.LogError("Loi Item");
            if (item.Type == Item.ItemType.Heal)
            {
                healthData.currentHP += 50f; 
                OnPlayerTakeDamage?.Invoke(0f);
            }
            if (item.Type == Item.ItemType.Thunder)
            {
                player.GetNewWeapon(1);
                Debug.Log("Get Thunder");
            }
            if (item.Type == Item.ItemType.Fire)
            {
                player.GetNewWeapon(2);
                Debug.Log("Get Fire");
            }
            if (item.Type == Item.ItemType.Ice)
            {
                player.GetNewWeapon(3);
                Debug.Log("Get Ice");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (sm.isInvisible)
        {
            return;
        }
        if (collision.CompareTag("Thunder"))
        {
            Debug.Log("Take dame by Thunder");
            
            OnPlayerTakeDame(30f);
        }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Take dame by trigger");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogError("No enemy scripts !!!!");
                return;
            }
            OnPlayerTakeDame(enemy.damage);
        }
        if (collision.CompareTag("Trap"))
            //Debug.Log("Take dame by trap");
        {
            ITrap trap = collision.GetComponent<ITrap>();
            if (trap != null)
                OnPlayerTakeDame(trap.MakeDamage());

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sm.isDeath) return;
        if (sm.isInvisible)
        {
            return;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy == null) 
            { 
                Debug.LogError("No enemy scripts !!!!");
                return;
            }
            OnPlayerTakeDame(enemy.damage);
        }
    }

    public void OnPlayerTakeDame(float damage)
    {
        healthData.currentHP -= damage;
        OnPlayerTakeDamage?.Invoke(damage);
        if (healthData.currentHP <= 0) 
        {
            Die();
            return;
        }
        sm.ChangeState(sm.getHitState);
        sm.StartInvisible(sm.invisibleTime);
        
    }

    public void Die()
    {
        //sm.ChangeState(sm.deathState);
        Debug.Log("Player Die !");
        OnPlayerDie?.Invoke();
    }
}
