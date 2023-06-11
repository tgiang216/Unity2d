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
        sm.rb.AddForce(new Vector2(0, sm.jumpForce), ForceMode2D.Impulse);
        sm.isJumping = true;
        sm.jumpTime = 0;
        sm.animator.Play("PlayerJump");

        Debug.Log("Enter Jump state");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKey(KeyCode.Space) && sm.IsGround())
        {
        
        }
        if (sm.isJumping)
        {
            //stateMachine.ChangeState(sm.jumpState);
          
            //sm.rb.velocity = new Vector2(sm.rb.velocity.x,sm.jumpForce);
            sm.jumpTime += Time.deltaTime;
        
            if(sm.jumpTime > sm.buttonPressTime)
            {
                sm.isJumping = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) | sm.jumpTime > sm.buttonPressTime)
        {
            sm.isJumping = false;
            sm.ChangeState(sm.fallState);

        }
        if(sm.rb.velocity.y < 0)
        {
            sm.ChangeState(sm.fallState); 
        }
        if (Input.GetMouseButton(0) && sm.canAirAtk)
        {
            sm.ChangeState(sm.airAtkState);
        }
    }
    public override void UpdatePhysics()
    {
       
        MoveLeft_Right();
    }
    public override void Exit()
    {
        base.Exit();
        sm.isJumping = false; 
        sm.jumpTime = 0;
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
