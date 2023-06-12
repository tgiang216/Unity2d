using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatCtrl : MonoBehaviour
{
    
    [SerializeField] float damage = 10;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] Transform thunderPos;
    [SerializeField] float thunderTime = 0.4f;
    [SerializeField] MovementSM sm;
    [SerializeField] LayerMask layer;
    public float maxHit = 0.06f;
    private float hitime = 0;


    public Transform hitPoint;
    private void Start()
    {
        sm = GetComponent<MovementSM>();
    }

    private void FixedUpdate()
    {
        hitime += Time.fixedDeltaTime;
        if (sm.isAttacking)
        {
            Time.timeScale = 0.5f;
            RaycastHit2D[] rays = Physics2D.CircleCastAll(hitPoint.position, 0.3f, Vector2.right, 0f, layer);
            foreach(RaycastHit2D ray in rays)
            {
                if (hitime >= maxHit)
                {
                    Vector2 hitpoit = ray.point;
                    ray.collider.GetComponent<Enemy>().TakeDamage(10f, hitpoit);
                    hitime = 0;
                    
                }
                else continue;
                
            }

            //Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, 0.3f, layer);
            //if (colliders.Length > 0)
            //{
            //    foreach(Collider2D collider in colliders)
            //    {
            //        collider.GetComponent<Enemy>().TakeDamage(10f, collider.);
            //    }
            //}
            Time.timeScale = 1f;
        }
        
    }

    public void CreateThunder()
    {
        GameObject effect = Instantiate(thunderPrefab,thunderPos.position,thunderPos.rotation);
        Destroy(effect, thunderTime);
    }
}
