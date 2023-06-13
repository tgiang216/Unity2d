using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateCtrl : StateMachine
{
    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public EnemyMove moveState;


    [Header("Enemy Setting")]
    public Animator animator;
    public Transform playerPos;
    public bool isPlayerInRange;
    public float rangeToSeePlayer;
    public float distanceToPlayer;

    [Header("Move Setting")]
    public float moveSpeed;
    public Transform targetToMove;
    //public List<Transform> pathPoint;

    [Header("Move Around Setting")]
    public bool isMovingAround;
    public Transform pointToAround;


    private void Awake()
    {
        moveState = new EnemyMove(this);
        idleState = new EnemyIdle(this);
    }
    protected override void StartSM()
    {
        animator= GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        pointToAround = transform;
        this.ChangeState(idleState);
    }

    protected override void UpdateSM()
    {
        if (targetToMove != null) return;
        if(isPlayerInRange)
        {
            this.ChangeState(moveState);
        }
    }


}
