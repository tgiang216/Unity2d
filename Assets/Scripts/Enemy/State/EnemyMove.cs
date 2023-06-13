using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseState
{
    private EnemyStateCtrl sm;
    private float distance;
    private Tween tween;
    public EnemyMove(EnemyStateCtrl state) : base("EnemyMove", state)
    {
        sm = (EnemyStateCtrl)state;
        
    }

    public override void Enter()
    {
        
        distance = Mathf.Abs(sm.transform.position.x - sm.targetToMove.x);
        sm.isMoving= true;
        if (sm.isMovingAround)
        {
            sm.targetToMove = new Vector3(Random.Range(sm.pointToAround.x - sm.moveRange, sm.pointToAround.x + sm.moveRange), 0);
            tween = sm.transform.DOMoveX(sm.targetToMove.x, 3f, false).OnComplete(OnMoveComplete);
        }

    }
    public override void UpdateLogic()
    {

        if (sm.isMovingAround && !sm.isMoving)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {
        sm.isMoving = false;
    }
    private void OnMoveComplete()
    {
        sm.isMoving = false;
    }
}

