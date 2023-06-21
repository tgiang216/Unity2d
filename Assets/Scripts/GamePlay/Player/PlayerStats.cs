using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float currentHP;

    [SerializeField]
    private PlayerStatesCtrl sm;
    [SerializeField]
    private PlayerCombatCtrl player;

    private void Awake()
    {
        sm = GetComponent<PlayerStatesCtrl>();
        player = GetComponent<PlayerCombatCtrl>();
    }
    private void Start()
    {
        currentHP = maxHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    //    if(sm.isInvisible)
    //    {
    //        return;
    //    }
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Take dame by trigger");
    //        Enemy enemy = collision.GetComponent<Enemy>();
    //        if (enemy == null)
    //        {
    //            Debug.LogError("No enemy scripts !!!!");
    //            return;
    //        }
    //        OnPlayerTakeDame(enemy.damage);
    //    }
    //    if (collision.CompareTag("Trap"))
    //        Debug.Log("Take dame by trap");
    //    {
    //        ITrap trap = collision.GetComponent<ITrap>();
    //        if (trap != null)
    //            OnPlayerTakeDame(trap.MakeDamage());

    //    }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (sm.isInvisible)
        {
            return;
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
            Debug.Log("Take dame by trap");
        {
            ITrap trap = collision.GetComponent<ITrap>();
            if (trap != null)
                OnPlayerTakeDame(trap.MakeDamage());

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        sm.ChangeState(sm.getHitState);
        sm.StartInvisible(sm.invisibleTime);
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Player Die !");
    }
}
