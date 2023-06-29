using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class EnemyStateCtrl : EnemyStateMachine
{
    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public EnemyMove moveState;
    [HideInInspector]
    public EnemySeePlayer foundState;
    [HideInInspector]
    public EnemyChasing chasingState;


    [Header("Enemy Setting")]
    public Animator animator;
    public Transform player;
    public float rangeToSeePlayer;
    public float distanceToPlayer;
    public Rigidbody2D rb;
    public bool isFacinRight = false;
    public bool isBoss =false;
    
    //public float localTimeScale = 1f;

    [Header("Move Setting")]
    public bool isMoving;
    public float moveSpeed;
    public Vector3 targetToMove;
    //public List<Transform> pathPoint;

    [Header("Move Around Setting")]
    public Vector3 pointToAround;
    public float moveRange;

    [Header("Idle Setting")]
    [HideInInspector]
    public float inIdleStateTime;

    [Header("Found Player")]
    [SerializeField] float foundCoolDown = 2f;

    [SerializeField] float foundTimer = 0;
    public bool isFoundPlayer;
    public float timeToPrepair = 1f;

    [Header("Attack Setting")]
    public bool isAttacking;

    [Header("Chasing Setting")]
    public bool isChasing;
    public float chasingSpeed;
    public int chasCount = 0;
    //public float chasingTime;

    protected virtual void Awake()
    {
        moveState = new EnemyMove(this);
        idleState = new EnemyIdle(this);
        foundState = new EnemySeePlayer(this);
        chasingState = new EnemyChasing(this);
    }
    protected override void StartSM()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindWithTag("Player").transform;
        pointToAround = transform.position;
        this.ChangeState(idleState);
        dieSoundName = "Pig";
    }

    protected override void UpdateSM()
    {
        if (isPlayerDeath) return;
        if (player == null) return;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //if (targetToMove != null) return;
        foundTimer += Time.deltaTime;
        if (IsPlayerInRange && !isFoundPlayer && foundTimer >= foundCoolDown && !isChasing )
        {
            foundTimer = 0;
            this.ChangeState(foundState);
            //Debug.Log("run...");
            
        }
        animator.speed = localTimeScale;
        //Debug.Log("Distance = " + distanceToPlayer);
    }

    public bool IsPlayerInRange => (distanceToPlayer < rangeToSeePlayer);
    
}

