using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseState
{
    private EnemyStateCtrl sm;
    private float distance;
    private Vector3 currentPos;
    private Tween tween;
    
    public EnemyMove(EnemyStateCtrl state) : base("EnemyMove", state)
    {
        sm = (EnemyStateCtrl)state;
        
    }

    public override void Enter()
    {
        Debug.Log("Enter Move state enemy");
        sm.animator.Play("BoarEnemyWalk");
        currentPos = sm.transform.position;
        distance = Mathf.Abs(sm.transform.position.x - sm.targetToMove.x);
        sm.isMoving= true;
                       
        if(currentPos.x < sm.targetToMove.x)
        {
            sm.isFacinRight = true;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sm.isFacinRight = false;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
        tween =  sm.rb.DOMoveX(sm.targetToMove.x,3f, false).OnComplete(OnMoveComplete);
      

    }
    public override void UpdateLogic()
    {
        if (sm.IsPlayerInRange)
        {
            if (tween.IsPlaying()) tween.Kill();
            sm.ChangeState(sm.foundState);
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
       
        sm.ChangeState(sm.idleState);       
        sm.isMoving = false;
    }

    private void MoveLeft_Right()
    {
        
    }
}

