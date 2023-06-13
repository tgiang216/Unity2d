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
    public bool isPlayerInRange = false;
    public float rangeToSeePlayer;
    public float distanceToPlayer;

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


    private void Awake()
    {
        moveState = new EnemyMove(this);
        idleState = new EnemyIdle(this);
    }
    protected override void StartSM()
    {
        animator= GetComponent<Animator>();
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        isMovingAround = true;
        pointToAround = transform.position;
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
