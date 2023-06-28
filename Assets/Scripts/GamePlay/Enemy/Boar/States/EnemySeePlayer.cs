using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySeePlayer : BaseState
{
    private EnemyStateCtrl sm;
    private float time;
    public EnemySeePlayer(StateMachine state) : base("Player Found", state)
    {
        sm = (EnemyStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Found Player");
        if(sm.mark != null) sm.mark.SetActive(true);
        sm.isFoundPlayer = true;
        time = 0;
        FaceToPlayer();
        //Player animation "Found Player"
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        if(time > sm.timeToPrepair)
        {
            sm.ChangeState(sm.chasingState);
        }
        if (!sm.IsPlayerInRange)
        {
            //sm.isFoundPlayer = false;
            sm.ChangeState(sm.idleState);
        }
    }
    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {
        sm.isFoundPlayer = false;
        if (sm.mark != null) sm.mark.SetActive(false);
    }
    private void FaceToPlayer()
    {
        if (sm.transform.position.x < sm.player.position.x)
        {
            sm.isFacinRight = true;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sm.isFacinRight = false;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
