using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : BaseState
{
    BoarBossStaresCtrl sm;
    private float timer = 0;
    public StunState(StateMachine state) : base("Stun", state)
    {
        sm = (BoarBossStaresCtrl)state;
    }

    public override void Enter()
    {
        Debug.Log("Stun State");
        sm.isStunning = true;
        sm.animator.Play("BoarEliteStun");
        timer = 0;
    }

    public override void UpdateLogic()
    {
        
        timer+= Time.deltaTime;
        if(timer > sm.stunTime)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {
        sm.isStunning = false;
    }
}
