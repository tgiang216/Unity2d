using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : BaseState
{
    private MovementSM sm;

    public Jumping(StateMachine state) : base("Jumping", state)
    {
        sm = (MovementSM) state;
        
    }
    public override void Enter()
    {
        base.Enter();
        sm.isJumping = true;
        sm.jumpTime = 0;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.isJumping = true;
            sm.jumpTime = 0;
        }
        if (sm.isJumping)
        {
            //stateMachine.ChangeState(sm.jumpState);
            sm.rb.velocity = new Vector2(sm.rb.velocity.x,sm.jumpForce);
            sm.jumpTime += Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.Space) | sm.jumpTime > sm.buttonPressTime)
        {
            sm.isJumping = false;
            sm.ChangeState(sm.fallState);
        }

    }

    public override void Exit()
    {
        base.Exit();
        sm.isJumping = false; 
        sm.jumpTime = 0;
    }

    public override void UpdatePhysics()
    {
        
    }
}
