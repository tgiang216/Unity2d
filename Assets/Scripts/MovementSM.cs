using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class MovementSM : StateMachine
{
    public Idle idleState;
    public Moving moveState;
}
