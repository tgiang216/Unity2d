using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private MovementSM sm;
    //private float horizontalInput;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) 
    {
        sm= stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Idle state");
        sm.horizontalInput = 0;
    }
    public override void UpdateLogic()
    {
        
        //horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log("horizontalInput" + horizontalInput);
        if(Mathf.Abs(sm.horizontalInput) > Mathf.Epsilon) 
        {
           
            sm.ChangeState(sm.moveState);
        }
        if(Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
    }
}
