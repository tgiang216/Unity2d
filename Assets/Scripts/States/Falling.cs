using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : BaseState
{
    private MovementSM sm;

    public Falling(StateMachine state) : base("Falling", state)
    {
        sm = (MovementSM) state;
    }
    public override void Enter()
    {
        base.Enter();
        sm.rb.gravityScale = 5f;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (sm.IsGround())
        {
            sm.ChangeState(sm.idleState);
        }
       
    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {
        base.Exit();
        sm.rb.gravityScale = 1f;
    }
}
