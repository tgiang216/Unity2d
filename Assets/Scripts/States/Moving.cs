using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private MovementSM sm;
    //private float horizontalInput;
    public Moving(MovementSM stateMachine) : base("Moving", stateMachine) 
    {
        sm = (MovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Move state");
        sm.horizontalInput = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        sm.horizontalInput = Input.GetAxis("Horizontal");
        if(Mathf.Abs(sm.horizontalInput) < Mathf.Epsilon)
        {
            sm.ChangeState(sm.idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 vel = sm.rb.velocity;
        vel.x = sm.horizontalInput * sm.moveSpeed;
        sm.rb.velocity = vel;
    }

   
}
