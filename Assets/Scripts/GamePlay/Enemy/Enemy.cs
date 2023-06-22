using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Setting")]
    public float maxHealth = 200f;
    private float curentHealth;
    public float damage;
    public StateMachine sm;
    public GameObject effect;
    public HealBarCtrl healthBar;

    private void Start()
    {
        healthBar.MaxValue = maxHealth;
        curentHealth = maxHealth;
    }
    public void TakeDamage(float damage , Vector2 positon)
    {
        
        curentHealth -= damage;
        //Debug.Log("curentHealth " + curentHealth);
        healthBar.SetHealthBar(curentHealth);
        Vector3 hitPos = positon;
        var flash = GetComponent<GetHitEffect>();
        if (flash == null)
        {
            Debug.LogError("effect loi");
        }
        flash.Flash();
        GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
        Destroy(hit, 0.2f);

        
        if(curentHealth <= 0)
        {
            Die();
        }

    }

    public void StunEffect()
    {
        
    }

    public void BurnEffect()
    {

    }

    public void Die()
    {
        Debug.Log(gameObject.name + " Die !");
        Destroy(gameObject);
    }
}
