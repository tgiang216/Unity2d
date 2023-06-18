using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSeePlayer : BaseState
{
    private BeeStateCtrl sm;
    private float timer = 0;

    public BeeSeePlayer(StateMachine state) : base("Bee See Player", state)
    {
        sm = (BeeStateCtrl)state;
    }
    public override void Enter()
    {
        Debug.Log("Found Player");
        sm.isFoundPlayer = true;
    }
    public override void UpdateLogic()
    {
        FaceToPlayer();
        timer += Time.deltaTime;
        if(timer >= sm.timeToPrepair) 
        {
            sm.ChangeState(sm.moveState);
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
