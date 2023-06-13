using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseState
{
    private EnemyStateCtrl sm;
    private float distance;
    public EnemyMove(EnemyStateCtrl state) : base("EnemyMove", state)
    {
        sm = (EnemyStateCtrl)state;
        distance = Mathf.Abs(sm.transform.position.x - sm.targetToMove.position.x);
    }

    public override void Enter()
    {
        sm.transform.DOMoveX(sm.targetToMove.position.x, distance / sm.moveSpeed, false);
    }
    public override void UpdateLogic()
    {
        
    }
    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {
        base.Exit();
    }

}

