using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : BaseState
{
    private MovementSM sm;
    private float time;
    private float animTime;
    public AirAttack(StateMachine state) : base("AirAttack", state)
    {
        sm = (MovementSM)state;
    }
    public override void Enter()
    {
        time = 0;
        sm.rb.velocity = Vector2.zero;
        sm.canAirAtk= false;
        sm.animator.Play("PlayerAtk1");
        animTime = sm.GetClipLenght("PlayerAtk1");
        sm.StartCoroutine(AirAtkCoolDown());
    }
    public override void UpdateLogic()
    {
         time+= Time.deltaTime;
        if (time > animTime)
        {
            sm.ChangeState(sm.fallState);
        }
    }
    public override void Exit()
    {

    }
    private IEnumerator AirAtkCoolDown()
    {
        yield return new WaitForSeconds(sm.airAtkCooldown);
        sm.canAirAtk = true;
    }
}
