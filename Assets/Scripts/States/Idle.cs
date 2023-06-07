using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private float horizontalInput;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) 
    {
        horizontalInput = 0f;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
