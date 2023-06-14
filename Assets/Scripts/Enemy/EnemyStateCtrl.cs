using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class EnemyStateCtrl : StateMachine
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

    [Header("Move Setting")]
    public bool isMoving;
    public float moveSpeed;
    public Vector3 targetToMove;
    //public List<Transform> pathPoint;

    [Header("Move Around Setting")]
    public bool isMovingAround;
    public Vector3 pointToAround;
    public float moveRange;

    [Header("Idle Setting")]
    public float inIdleStateTime;

    [Header("Found Player")]
    public bool isFoundPlayer;
    public float timeToPrepair = 1f;

    [Header("Attack Setting")]
    public bool isAttacking;

    [Header("Chasing Setting")]
    public bool isChasing;
    public float chasingSpeed;

    private void Awake()
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
        isMovingAround = true;
        pointToAround = transform.position;
        this.ChangeState(idleState);
    }

    protected override void UpdateSM()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (targetToMove != null) return;
        if (IsPlayerInRange)
        {
            this.ChangeState(foundState);
        }
        //Debug.Log("Distance = " + distanceToPlayer);
    }

    public bool IsPlayerInRange => (distanceToPlayer < rangeToSeePlayer);
    
}

