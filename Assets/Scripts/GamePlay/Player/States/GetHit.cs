using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : BaseState
{
    private PlayerStatesCtrl sm;
    private float time;
    private float dir;

    public GetHit(StateMachine state) : base("GetHit", state)
    {
        sm= (PlayerStatesCtrl)state;
    }

    public override void Enter()
    {
        sm.GetComponent<GetHitEffect>().Flash();
        dir = (sm.isFacingRight ? 1 : -1);
        Debug.Log("Enter gethit state");
        sm.horizontalInput = 0;
        sm.rb.velocity = Vector2.zero;
        sm.isGettingHit= true;
        Vector2 force = new Vector2(-sm.hitForce*dir*2, sm.hitForce);
        sm.rb.AddForce(force);
        sm.animator.Play("PlayerGetHit");
        AudioManager.Instance.PlaySE("PlayerGetHit");
        time = 0;
    }
    public override void UpdateLogic()
    {
        time += Time.deltaTime;
        if (time >= sm.timeRecover)
        {
            sm.ChangeState(sm.idleState);
        }
    }
    public override void Exit()
    {
        sm.isGettingHit= false;
    }
}
