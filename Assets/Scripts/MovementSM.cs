using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving moveState;
    [HideInInspector]
    public Jumping jumpState;
    [HideInInspector]
    public Falling fallState;

    [Header("Player Setting")]
    public Rigidbody2D rb;
    public BoxCollider2D boxcollider2D;
    public float horizontalInput;
    public Animator animator;
    public SpriteRenderer renderer;
    public bool isFacingRight = true;

    [Header("Move Setting")]
    public float moveSpeed = 4f;


    [Header("Jump Setting")]
    public float jumpTime;
    public bool isJumping;
    public float buttonPressTime = 0.3f;
    public float jumpForce = 10f;
    public LayerMask jumpAbleGround;
    private void Awake()
    {
        idleState = new Idle(this);
        moveState = new Moving(this);
        jumpState = new Jumping(this);
        fallState = new Falling(this);
    }
    protected override void StartSM()
    {
        Debug.Log("Get Component Chile");
        rb=GetComponent<Rigidbody2D>();
        boxcollider2D=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        renderer=GetComponent<SpriteRenderer>();
        this.ChangeState(idleState);
    }
    protected override void UpdateSM()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("horizontalInput "+ horizontalInput);
        Debug.Log("velocity " + rb.velocity);
    }
    public bool IsGround()
    {
        //Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (capsuleCollider2D.bounds.size.y / 2f + 0.1f), Color.red);
        //return Physics2D.BoxCast(capsuleCollider2D.bounds.center,
        // capsuleCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
        Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down * (boxcollider2D.bounds.size.y / 2f + 0.1f), Color.red);
        return Physics2D.BoxCast(boxcollider2D.bounds.center,
        boxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
    }
    //protected override BaseState GetInitState()
    //{
    //    Debug.Log("Innit state");
    //    return idleState;
    //}
}
