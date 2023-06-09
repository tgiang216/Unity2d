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
        sm.rb.gravityScale = 10f;
        sm.animator.Play("PlayerFall");
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
        MoveLeft_Right();   
    }
    public override void Exit()
    {
        base.Exit();
        sm.rb.gravityScale = 1f;
    }
    private void MoveLeft_Right()
    {
        Vector2 vel = sm.rb.velocity;
        if (vel.x > 0f)
        {
            sm.isFacingRight = true;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (vel.x < 0f)
        {
            sm.isFacingRight = false;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        vel.x = sm.horizontalInput * sm.moveSpeed;
        sm.rb.velocity = vel;
    }
}
