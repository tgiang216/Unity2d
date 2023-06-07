using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private float horizontalInput;
    public Moving(MovementSM stateMachine) : base("Moving", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalInput = Input.GetAxis("Horizontal");
        if(Mathf.Abs(horizontalInput) < Input.GetAxis("Horizontal"))
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).moveState);

        }
    }
}
