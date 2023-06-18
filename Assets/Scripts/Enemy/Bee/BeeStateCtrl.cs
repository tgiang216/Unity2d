using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStateCtrl : StateMachine
{
    [HideInInspector]
    public BeeIdle idleState;
    [HideInInspector]
    public BeeMove moveState;
    [HideInInspector]
    public BeeSeePlayer foundState;
    [HideInInspector]
    public BeeAttack atkState;
    [HideInInspector]
    public BeeDeath deathState;
    [HideInInspector]
    public BeeGetHit gethitState;


    [Header("Enemy bee Setting")]
    public Animator animator;
    public Transform player;
    public float rangeToSeePlayer;
    public float distanceToPlayer;
    public Rigidbody2D rb;
    public bool isFacinRight = false;
    public BeeHiveCtrl beeHive;

    [Header("Move Setting")]
    public bool isMoving;
    public float moveSpeed;
    public Vector3 targetToMove;
    //public List<Transform> pathPoint;

    [Header("Move Around Setting")]
    public Transform pointToAround;
    public float moveRange;

    [Header("Idle Setting")]
    public float inIdleStateTime;

    [Header("Found Player")]
    public bool isFoundPlayer;
    public float timeToPrepair = 1f;

    [Header("Attack Setting")]
    public bool isAttacking;
    public float atkRange;

    [Header("Get hit setting")]
    public float hitRecoverTime;
    public bool isGettingHit;
    private void Awake()
    {
        idleState= new BeeIdle(this);
        moveState= new BeeMove(this);
        foundState= new BeeSeePlayer(this);
        atkState= new BeeAttack(this);
        deathState = new BeeDeath(this);
        gethitState = new BeeGetHit(this);
    }
    protected override void StartSM()
    {
        base.StartSM();
        rb=GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        pointToAround = transform;
        player = GameObject.FindWithTag("Player").transform;
        this.ChangeState(idleState);       
    }
    

    protected override void UpdateSM()
    {
        
    }
    private void LateUpdate()
    {
        if (beeHive == null) return;
        if (isFoundPlayer) return;
        if (beeHive.IsPlayerInRange && !isFoundPlayer)
        {
            this.ChangeState(foundState);
        }
        else
        if(!beeHive.IsPlayerInRange)
        {
            this.ChangeState(idleState);
            isFoundPlayer = false;
        }
            

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(pointToAround != null) Gizmos.DrawSphere(pointToAround.position, 0.2f);
    }

    private void OnDestroy()
    {if(beeHive != null)
        {
            beeHive.OnABeeDeath(this.gameObject);
        }       
    }
}
