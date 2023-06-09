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
        //sm.horizontalInput = 0f;
        //sm.animator.SetTrigger("Running");
        sm.animator.Play("PlayerRun");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //sm.horizontalInput = Input.GetAxis("Horizontal");
        if(sm.horizontalInput == 0 && sm.IsGround())
        {
            sm.ChangeState(sm.idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space) && sm.IsGround())
        {
            sm.ChangeState(sm.jumpState);
        }
        if(sm.rb.velocity.y < -0.2f)
        {
            sm.ChangeState(sm.fallState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        MoveLeft_Right();
    }
    private void MoveLeft_Right()
    {
        Vector2 vel = sm.rb.velocity;
        if (sm.horizontalInput > 0f)
        {
            sm.isFacingRight = true;
            sm.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (sm.horizontalInput < 0f)
        {
            sm.isFacingRight = false;
            sm.transform.localScale = new Vector3(-1, 1, 1);
        }
        vel.x = sm.horizontalInput * sm.moveSpeed;
        sm.rb.velocity = new Vector2(vel.x, sm.rb.velocity.y);
    }
   
}
