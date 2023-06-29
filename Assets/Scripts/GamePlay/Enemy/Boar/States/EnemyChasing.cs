using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.TextCore.Text;

public class EnemyChasing : BaseState
{
    protected EnemyStateCtrl sm;
    private Vector2 target;
    private float time;
    private float offset;
    Tween tween;
    public EnemyChasing(EnemyStateCtrl state) : base("Chasing Player", state)
    {
        sm = (EnemyStateCtrl)state;
    }

    public override void Enter()
    {
       //Debug.Log("Enter enemy chasing ");
        sm.isChasing = true;
        sm.chasCount++;
        target = sm.player.position;
        FaceToPlayer();
        sm.animator.Play("BoarEnemyRun");
        
        //offset = sm.isBoss? 7f : 3f;
        float chaseTime = (sm.distanceToPlayer + offset) / sm.chasingSpeed;
        float actionTime = (sm.localTimeScale == 0f) ? -100f : sm.localTimeScale;
        tween = sm.rb.DOMoveX(target.x + offset, chaseTime * (2f - actionTime)).OnComplete(OnMoveComplete);
        time = 0f;
       
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        if (time >= 2f) 
        {
            sm.isChasing = false;
            sm.ChangeState(sm.idleState);
            //if (!sm.IsPlayerInRange)
            //{
            //    time = 0f;
            //    sm.isChasing = false;
            //    sm.isFoundPlayer= false;
            //    sm.ChangeState(sm.idleState);
            //}          
        }
    }
    public override void Exit()
    {
        sm.isChasing = false;
        if (tween != null) tween.Kill();
    }
    private void OnMoveComplete()
    {
        if (sm.isChasing) return;
        sm.ChangeState(sm.idleState);
        //if (!sm.IsPlayerInRange)
        //{
        //    sm.ChangeState(sm.idleState);

        //}   
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
