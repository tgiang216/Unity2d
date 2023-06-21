using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BeeIdle : BaseState
{
    private BeeStateCtrl sm;
    private float timer;
    public BeeIdle(StateMachine state) : base("Bee Idle", state)
    {
        sm = (BeeStateCtrl) state;
    }
    public override void Enter()
    {
        //Debug.Log("Enter IDLe state Bee");
        sm.animator.Play("BeeIdle");
        timer = 0;
        sm.inIdleStateTime = Random.Range(2f, 5f);
    }
    public override void UpdateLogic()   
    {
        timer += Time.deltaTime;
        if(sm.isFoundPlayer) { sm.inIdleStateTime = 1f; }
        if (timer > sm.inIdleStateTime)
        {
           // Debug.Log("Change to move");
            sm.ChangeState(sm.moveState);
        }
    }
    public override void Exit()
    {
        timer = 0f;
    }
}
