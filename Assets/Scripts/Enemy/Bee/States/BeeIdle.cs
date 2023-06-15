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
        Debug.Log("Enter IDLe state Bee");
        //sm.animator.Play("BoarEnemyIdle");
        timer = 0;
        sm.inIdleStateTime = Random.Range(2f, 5f);
    }
    public override void UpdatePhysics()
    {
        timer += Time.deltaTime;
        if (timer > sm.inIdleStateTime)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            randomDirection.y = Mathf.Clamp(randomDirection.y, -Mathf.Sqrt(3) / 2, 0f); 
            Vector2 randomPoint = (Vector2)sm.pointToAround.position + randomDirection * sm.moveRange;

            sm.targetToMove = randomPoint;
            sm.ChangeState(sm.moveState);
        }
    }
    public override void Exit()
    {

    }
}
