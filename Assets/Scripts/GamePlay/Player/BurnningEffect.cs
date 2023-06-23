using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BurnningEffect : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float delay = 0.2f;
    [SerializeField]
    LayerMask enemyMask;
    [SerializeField]
     private float radius;

    private float time = 0;

    private bool canMakeDamage;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        if (time >= delay)
        {
            MakeDamage();
            time = 0f;
        }
        
    }

    void MakeDamage()
    {              
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
        if(colliders.Length > 0)
        {
            foreach(Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if(enemy != null)
                    enemy.TakeDamage(damage, Vector2.zero);
            }
        }     
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
