using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeeMove : BaseState
{
    private BeeStateCtrl sm;
    private float distance;
    private Vector3 currentPos;
    private Tween tween;
    public BeeMove(StateMachine state) : base("Bee Move", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Enter Move state Bee");
        //sm.animator.Play("BoarEnemyWalk");
        currentPos = sm.transform.position;
        distance = Mathf.Abs(sm.transform.position.x - sm.targetToMove.x);
        sm.isMoving = true;

        if (currentPos.x < sm.targetToMove.x)
        {
            sm.isFacinRight = true;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sm.isFacinRight = false;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
        tween = sm.rb.DOMove(sm.targetToMove, 3f, false).OnComplete(OnMoveComplete);
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
