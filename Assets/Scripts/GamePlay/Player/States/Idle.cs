using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private PlayerStatesCtrl sm;
    //private float horizontalInput;
    public Idle(PlayerStatesCtrl stateMachine) : base("Idle", stateMachine) 
    {
        sm = (PlayerStatesCtrl)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Idle state");
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
        if (sm.atkKeyPressed && !sm.isAttacking && sm.IsGround())
        {
            sm.ChangeState(sm.groundAtk1);
        }
        if(Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
        if (Input.GetKeyDown(KeyCode.K) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
        if (sm.rb.velocity.y < -0.1f)
        {
            sm.ChangeState(sm.fallState);
        }
    }
}
