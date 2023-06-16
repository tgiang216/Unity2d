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
    public float timeScaleRate = 0.8f;
    public float timeSlowMotion = 1f;
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
            
            RaycastHit2D[] rays = Physics2D.CircleCastAll(hitPoint.position, 0.3f, Vector2.right, 0f, layer);
            foreach(RaycastHit2D ray in rays)
            {
                if (hitime >= maxHit)
                {
                    StartCoroutine(SlowMotion(timeSlowMotion));
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
           
        }
        
    }

    public void CreateThunder()
    {
        GameObject effect = Instantiate(thunderPrefab,thunderPos.position,thunderPos.rotation);
        Destroy(effect, thunderTime);
    }
    private IEnumerator SlowMotion(float time)
    {       
        Time.timeScale = timeScaleRate;
        Debug.Log("Slowmotion : " + Time.timeScale);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
    }
}
