using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : BaseState
{
    private EnemyStateCtrl sm;
    private Vector2 target;
    private float time;
    private float offset;
    public EnemyChasing(EnemyStateCtrl state) : base("Chasing Player", state)
    {
        sm = (EnemyStateCtrl)state;
    }

    public override void Enter()
    {
        //Debug.Log("Enter enemy chasing ");
        sm.isChasing = true;
        target = sm.player.position;
        FaceToPlayer();
        sm.animator.Play("BoarEnemyRun");
        sm.rb.DOMoveX(target.x + offset, 1f).OnComplete(OnMoveComplete);
        time = 0f;
       
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        if (time >= 2f) 
        {
            if (!sm.IsPlayerInRange)
            {
                time = 0f;
                sm.isChasing = false;
                sm.isFoundPlayer= false;
                sm.ChangeState(sm.idleState);
            }          
        }
    }
    public override void Exit()
    {
        
    }
    private void OnMoveComplete()
    {
        sm.isChasing = false;
        if (!sm.IsPlayerInRange)
        {
            sm.ChangeState(sm.idleState);

        }
        else
        {
            sm.ChangeState(sm.foundState);
        }
        
    }

    private void FaceToPlayer()
    {
        if (sm.transform.position.x < sm.player.position.x)
        {
            sm.isFacinRight = true;
            sm.transform.localScale = new Vector3(-1, 1, 1);
            offset = 2f;
        }
        else
        {
            sm.isFacinRight = false;
            sm.transform.localScale = new Vector3(1, 1, 1);
            offset = -2f;
        }
    }
}
