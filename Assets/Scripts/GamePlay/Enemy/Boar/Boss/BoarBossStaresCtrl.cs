using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBossStaresCtrl : EnemyStateCtrl
{
    [HideInInspector]
    public StunState stunState;
    [HideInInspector]
    public ThunderAtkState atkState;

    public float wallCollisionCooldown = 5f;
    private float collisionTimer = 0f;

    [Header("Stun Setting")]
    public float stunTime;
    public bool isStunning;
    [SerializeField]private int stunCount;
    public int stunMax; 

    [Header("Thunder atk setting")]
    public float atkCount;
    public float atkTnteval;
    public bool isThunderatk;
    public BackgroundCtrl backgroundCtrl;
    public GameObject thunderPrefab;
    protected override void Awake()
    {
        base.Awake();
        stunState = new StunState(this);
        atkState = new ThunderAtkState(this);
    }
    protected override void StartSM()
    {
        base.StartSM();
        stunCount= 0;
        collisionTimer = wallCollisionCooldown;
    }

    protected override void UpdateSM()
    {
        
        if (isStunning) return;
        if (isThunderatk) return;
        collisionTimer += Time.deltaTime;
        if (stunCount >= stunMax || chasCount >=3f)
        {
            stunCount = 0;
            chasCount= 0;
            ChangeState(atkState);
            return;
        }
        base.UpdateSM();

    }
    
    public void OnWallCollision()
    {
        if(collisionTimer > wallCollisionCooldown)
        {
            
            collisionTimer = 0;
            //Dung tuong
            this.ChangeState(stunState);
            stunCount++;
        }
        
    }
    public void CreateThunder(Vector3 position)
    {
        GameObject effect = Instantiate(thunderPrefab, position, Quaternion.identity);
        Destroy(effect, 0.4f);
        backgroundCtrl.ThunderFlash();
        backgroundCtrl.CameraShake();
        Debug.Log("create thunder");
    }

    
}
