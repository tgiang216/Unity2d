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
        //Debug.Log("Found Player");
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
    }
    public override void UpdatePhysics()
    {
        if (!sm.IsPlayerInRange)
        {
            sm.isFoundPlayer = false;
            sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {

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
