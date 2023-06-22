using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatCtrl : MonoBehaviour
{
    [SerializeField] float damage = 20f;

    [Header("Thunder setting")]        
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] float rate;
    [SerializeField] GameObject thunderHitPrefab;
    // [SerializeField] float thunderTime = 0.4f;


    [SerializeField] PlayerStatesCtrl sm;
    [SerializeField] LayerMask layer;
    public float maxHit = 0.06f;
    public float timeScaleRate = 0.8f;
    public float timeSlowMotion = 1f;
    private float hitime = 0;
    public Transform hitBox;
    public WeaponType weapon;
    public enum WeaponType
    {
        Normal,
        Thunder,
        Ice,
        Fire
    }

    
    private void Start()
    {
        sm = GetComponent<PlayerStatesCtrl>();
    }

    private void FixedUpdate()
    {
        hitime += Time.fixedDeltaTime;
        if (sm.isAttacking)
        {
            Attack();                          
        }       
    }

    private void Attack()
    {
        RaycastHit2D[] rays = Physics2D.CircleCastAll(hitBox.position, 0.3f, Vector2.right, 0f, layer);
        foreach (RaycastHit2D ray in rays)
        {
            if (ray.collider.tag != "Enemy") continue;
            if (hitime >= maxHit)
            {
                StartCoroutine(SlowMotion(timeSlowMotion));
                Vector2 hitpoit = ray.point;
                ray.collider.GetComponent<Enemy>().TakeDamage(damage, hitpoit);
                AttackHitEffect(hitpoit);
                AttackEffect(hitpoit);
                hitime = 0;

            }
            else continue;
        }
    }

   public void AttackEffect(Vector3 hitpoint)
    {
        switch(weapon)
        {
            case WeaponType.Normal:
                break;
            case WeaponType.Thunder:
                int rand = Random.Range(0, 100);
                if (rate > rand)
                {
                    CreateThunder(hitpoint);
                }
                break;
            case WeaponType.Fire: break;
            case WeaponType.Ice: break;
            
        }
    }
    public void AttackHitEffect(Vector3 position)
    {
        switch (weapon)
        {
            case WeaponType.Normal:
                break;
            case WeaponType.Thunder:
                GameObject effect = Instantiate(thunderHitPrefab, position, Quaternion.identity);
                Destroy(effect, 0.2f);
                break;
            case WeaponType.Fire: break;
            case WeaponType.Ice: break;

        }
    }
    public void CreateThunder(Vector3 position)
    {
        GameObject effect = Instantiate(thunderPrefab, position, Quaternion.identity);
        Destroy(effect, 0.4f);
    }
    
    private IEnumerator SlowMotion(float time)
    {       
        Time.timeScale = timeScaleRate;
        //Debug.Log("Slowmotion : " + Time.timeScale);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
    }
}
