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
    [Header("Fire setting")]
    [SerializeField] GameObject firePrefab;
    [SerializeField] float burnRate;
    [SerializeField] float burnDuration;

    [SerializeField] PlayerStatesCtrl sm;
    [SerializeField] LayerMask layer;
    public float maxHit = 0.06f;
    public float timeScaleRate = 0.8f;
    public float timeSlowMotion = 1f;
    private float hitime = 0;
    public Transform hitBox;

    public GameObject ThunderEffect;
    public GameObject IceEffect;
    public GameObject FireEffect;

    private GameObject currentEffect;
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
        ThunderEffect.SetActive(false);
        IceEffect.SetActive(false);
        FireEffect.SetActive(false);
        sm = GetComponent<PlayerStatesCtrl>();
        weapon = WeaponType.Normal;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeWeapon(WeaponType.Thunder);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeWeapon(WeaponType.Ice);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeWeapon(WeaponType.Fire);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            ChangeWeapon(WeaponType.Normal);
        }
    }

    private void FixedUpdate()
    {
        hitime += Time.fixedDeltaTime;
        if (sm.isAttacking)
        {
            Attack();                          
        }
       
    }

    private void ChangeWeapon(WeaponType type)
    {
        weapon = type;

        if(weapon == WeaponType.Normal)
        {
            ThunderEffect.SetActive(false);
            IceEffect.SetActive(false);
            FireEffect.SetActive(false);
        }
        if (weapon == WeaponType.Thunder)
        {
            ThunderEffect.SetActive(true);
            IceEffect.SetActive(false);
            FireEffect.SetActive(false);
        }
        if (weapon == WeaponType.Ice)
        {
            ThunderEffect.SetActive(false);
            IceEffect.SetActive(true);
            FireEffect.SetActive(false);
        }
        if (weapon == WeaponType.Fire)
        {
            ThunderEffect.SetActive(false);
            IceEffect.SetActive(false);
            FireEffect.SetActive(true);
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
                Enemy enemy = ray.collider.GetComponent<Enemy>();
                enemy.TakeDamage(damage, hitpoit);
                AttackHitEffect(hitpoit);
                AttackEffect(hitpoit, enemy);
                hitime = 0;

            }
            else continue;
        }
    }

   public void AttackEffect(Vector3 hitpoint, Enemy enemy)
    {
        int rand = Random.Range(0, 100);
        switch (weapon)
        {
            case WeaponType.Normal:
                break;
            case WeaponType.Thunder:
                
                if (rate > rand)
                {
                    CreateThunder(hitpoint);
                    enemy.TakeDamage(damage, hitpoint);
                }
                break;
            case WeaponType.Fire:
                if(burnRate > rand)
                {
                    CreateBurn(hitpoint, enemy);
                }
                break;
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
                GameObject thundereffect = Instantiate(thunderHitPrefab, position, Quaternion.identity);
                Destroy(thundereffect, 0.2f);
                break;
            case WeaponType.Fire:
                GameObject fireeffect = Instantiate(thunderHitPrefab, position, Quaternion.identity);
                Destroy(fireeffect, 0.2f);
                break;
            case WeaponType.Ice: break;

        }
    }
    public void CreateThunder(Vector3 position)
    {
        GameObject effect = Instantiate(thunderPrefab, position, Quaternion.identity);
        Destroy(effect, 0.4f);
    }
    public void CreateBurn(Vector3 position , Enemy enemy)
    {
        GameObject effect = Instantiate(firePrefab, position, Quaternion.identity);
        Destroy(effect, burnDuration);
    }
    private IEnumerator SlowMotion(float time)
    {       
        Time.timeScale = timeScaleRate;
        //Debug.Log("Slowmotion : " + Time.timeScale);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
    }
}
