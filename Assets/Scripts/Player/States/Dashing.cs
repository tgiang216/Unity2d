using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : BaseState
{
    private PlayerStatesCtrl sm;
    private float dashTime;
    private float gravityHold;
    private int dashDir;
    
    public Dashing(StateMachine state) : base("Dash", state)
    {
        sm = (PlayerStatesCtrl)state;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter dash state");
        gravityHold = sm.rb.gravityScale;
        sm.rb.velocity = Vector2.zero;
        sm.horizontalInput = 0;
        sm.rb.gravityScale = 0f;
        dashDir = (sm.isFacingRight? 1 : -1);
        sm.isDashing = true;
        sm.canDash = false;
        dashTime = 0f;
        sm.StartCoroutine(DashCoolDawn());
        sm.animator.Play("PlayerDash");

    }
    public override void UpdateLogic()
    {
        dashTime += Time.deltaTime;
       
        if(dashTime <= sm.dashTime)
        {
            sm.rb.velocity = new Vector2(sm.dashForce * dashDir, 0f);
        }
        else
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
        sm.isDashing = false;
        sm.rb.gravityScale = gravityHold;
    }

    private IEnumerator DashCoolDawn()
    {
        yield return new WaitForSeconds(sm.dashCoolDown);
        sm.canDash = true;
    }
}
