using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Setting")]
    public float maxHealth = 200f;
    private float curentHealth;
    public EnemyStateCtrl stateCtrl;
    public GameObject effect;
    public HealBarCtrl healthBar;

    private void Start()
    {
        healthBar.MaxValue = maxHealth;
        curentHealth = maxHealth;
        stateCtrl= GetComponent<EnemyStateCtrl>();
    }
    public void TakeDamage(float damage , Vector2 positon)
    {
        
        curentHealth -= damage;
        Debug.Log("curentHealth " + curentHealth);
        healthBar.SetHealthBar(curentHealth);
        Vector3 hitPos = positon;
        GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
        Destroy(hit, 0.2f);
        if(curentHealth <= 0)
        {
            Die();
        }

    }
    public void Die()
    {
        Debug.Log("Boar die");
        Destroy(gameObject);
    }
}
