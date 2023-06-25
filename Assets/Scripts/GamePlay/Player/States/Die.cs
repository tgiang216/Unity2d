using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : BaseState
{
    PlayerStatesCtrl sm;
    public Die(StateMachine state) : base("Death ", state)
    {
        sm = (PlayerStatesCtrl)state;
    }
    public override void Enter()
    {
        sm.animator.Play("PlayerDie");
        sm.isDeath = true;
        sm.rb.gravityScale = 0f;
        sm.rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        sm.isDeath = false;
        sm.rb.gravityScale = 1f;
    }
}
