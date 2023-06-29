using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCombatCtrl : MonoBehaviour
{
    [SerializeField] float damage = 20f;

    [Header("Thunder setting")]        
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] float rate;
    [SerializeField] GameObject thunderHitPrefab;
    [SerializeField] BackgroundCtrl backgroundCtrl;
    // [SerializeField] float thunderTime = 0.4f;
    [Header("Fire setting")]
    [SerializeField] GameObject firePrefab;
    [SerializeField] float burnRate;
    [SerializeField] float burnDuration;
    [SerializeField] GameObject fireHitPrefab;

    [Header("Slow setting")]
    [SerializeField] float freezingRate;
    [SerializeField] float slowDuration;
    [SerializeField] GameObject iceBlockPrefab;


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

    public bool canUseThunder = false;
    public bool canUseFire = false;
    public bool canUseIce = false;

    public static event Action<int> OnChangeWeapon;
    public static event Action<int> OnGetNewWeapon;
    public WeaponType weapon;
    public enum WeaponType
    {
        Normal,
        Thunder,
        Fire,
        Ice
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
            if (!canUseThunder) return;
            ChangeWeapon(WeaponType.Thunder);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (!canUseFire) return;
            ChangeWeapon(WeaponType.Fire);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (!canUseIce) return;
            ChangeWeapon(WeaponType.Ice);
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

    public void GetNewWeapon(int type)
    {
        if (type == 1) canUseThunder = true;
        if (type == 2) canUseFire = true;
        if (type == 3) canUseIce = true;
        OnGetNewWeapon?.Invoke(type);
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
        OnChangeWeapon?.Invoke((int)weapon);
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
                if (enemy == null) { Debug.LogError("No find enemy"); }
                enemy.TakeDamage(damage, hitpoit);
                AttackHitEffect(hitpoit,enemy);
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
            case WeaponType.Ice:
                if(freezingRate> rand)
                {
                    CreateIceBlock(hitpoint, enemy);
                }
                
                //enemy.ActiveSlowEffect(5f, 0f);
                break;
            
        }
    }
    public void AttackHitEffect(Vector3 position, Enemy enemy)
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
                enemy.ActiveBurnEffect(3f, 1f);
                GameObject fireeffect = Instantiate(fireHitPrefab, position, Quaternion.identity);
                fireeffect.transform.parent = enemy.transform;
                Destroy(fireeffect, 3f);
                break;
            case WeaponType.Ice:
                enemy.ActiveSlowEffect(3f, 0.5f);
                break;

        }
    }
    public void CreateThunder(Vector3 position)
    {
        GameObject effect = Instantiate(thunderPrefab, position, Quaternion.identity);
        Destroy(effect, 0.4f);
        backgroundCtrl.ThunderFlash();
        backgroundCtrl.CameraShake();
    }
    public void CreateBurn(Vector3 position , Enemy enemy)
    {
        GameObject effect = Instantiate(firePrefab, position, Quaternion.identity);
        Destroy(effect, burnDuration);
    }
    public void CreateIceBlock(Vector3 position , Enemy enemy)
    {
        if(enemy.isFreezing)
        {
            enemy.TakeDamage(damage,Vector2.zero);
            return;
        }
        enemy.ActiveSlowEffect(slowDuration, 0f);
        GameObject effect = Instantiate(iceBlockPrefab, position, Quaternion.identity);
        effect.transform.parent = enemy.transform;
        if(effect != null)
        {
            Destroy(effect, slowDuration);
        }
        
    }
    private IEnumerator SlowMotion(float time)
    {       
        Time.timeScale = timeScaleRate;
        //Debug.Log("Slowmotion : " + Time.timeScale);
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
    }
   
}
