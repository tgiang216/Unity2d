using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGetHit : BaseState
{
    private BeeStateCtrl sm;
    private float timer = 0;
    public BeeGetHit(StateMachine state) : base("Bee Get Hit", state)
    {
       sm = (BeeStateCtrl)state;
    }

    public override void Enter()
    {
        timer = 0;
        sm.isGettingHit= true;
        sm.animator.Play("BeeGetHit");
    }
    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if(timer > sm.hitRecoverTime)
        {
            if(sm.isFoundPlayer)
            {
                sm.ChangeState(sm.foundState);
            }else
                sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {
        sm.isGettingHit = false;
    }
}
