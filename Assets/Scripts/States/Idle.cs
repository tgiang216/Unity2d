using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private MovementSM sm;
    //private float horizontalInput;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) 
    {
        sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Idle state");
        sm.horizontalInput = 0;
        sm.rb.velocity = Vector2.zero;
        sm.animator.Play("PlayerIdle");
        //sm.animator.SetTrigger("Idle");
    }
    public override void UpdateLogic()
    {
        
        //horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log("horizontalInput" + horizontalInput);
        if(sm.horizontalInput != 0 && sm.IsGround()) 
        {         
            sm.ChangeState(sm.moveState);
        }
        if(Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
        if (sm.rb.velocity.y < -0.2f)
        {
            sm.ChangeState(sm.fallState);
        }
    }
}
