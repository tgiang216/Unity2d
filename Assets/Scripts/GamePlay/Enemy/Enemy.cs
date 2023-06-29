using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Setting")]
    public float maxHealth = 200f;
    private float curentHealth;
    public float damage;
    public EnemyStateMachine sm;
    //public GameObject effect;
    public GameObject dieEfectPrefab;
    public HealBarCtrl healthBar;
    private SpriteRenderer renderer;
    [Header("Burning Status setting")]
    public bool isBurning = false;  
    public float burnInterval = 0.2f;

    [Header("Slow Status setting")]
    public Color slowColor;
    private Color originColor;
    public bool isSlowing = false;
    public bool isFreezing = false;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        sm = GetComponent<EnemyStateMachine>();
    }
    private void Start()
    {
        healthBar.MaxValue = maxHealth;
        curentHealth = maxHealth;
        originColor = renderer.color;
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
        //GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
        //Destroy(hit, 0.2f);

        
        if(curentHealth <= 0)
        {
            Die();
        }

    }

    public void StunEffect()
    {
        
    }

    private Coroutine slowCoroutine;
    public void ActiveSlowEffect(float slowTime, float slowRate)
    {
        if(isFreezing) { return; }
        if(isSlowing)
        {
            StopCoroutine(slowCoroutine);
            //isSlowing= false;
        }
        slowCoroutine = StartCoroutine(SlowCoroutine(slowTime, slowRate));
    }

    public void ActiveBurnEffect(float burningTime, float burnDamage)
    {
        StartCoroutine(BurnCorountine(burningTime, burnDamage));
    }

    public void Die()
    {
        if(sm != null) AudioManager.Instance.PlaySE(sm.dieSoundName);
        GameObject hit = Instantiate(dieEfectPrefab, transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);       
       Debug.Log(gameObject.name + " Die !");
        Destroy(gameObject);
        
        
    }

    private IEnumerator BurnCorountine(float burningTime, float burnDamage)
    {
        isBurning = true;
        float timer = 0f;
        while(timer < burningTime)
        {
            this.TakeDamage(burnDamage, Vector2.zero);
            yield return new WaitForSeconds(burnInterval);
            timer += burnInterval;
        }
        isBurning = false;
    }

    private IEnumerator SlowCoroutine(float slowTime, float slowRate)
    {
        if(slowRate <= 0.2f)
        {
            isFreezing= true;
        }
        isSlowing = true;

        if(sm != null)
        {
            sm.localTimeScale = slowRate;
            //sm.SetActionSpeed(sm.localTimeScale);
        }      
        renderer.color = slowColor;
        yield return new WaitForSeconds(slowTime);
        if(sm != null)
        {
            sm.localTimeScale = 1f;
            //sm.SetActionSpeed(sm.localTimeScale);
        }
        
        renderer.color = originColor;
        isFreezing = false;
        isSlowing = false;
    }

   
}
