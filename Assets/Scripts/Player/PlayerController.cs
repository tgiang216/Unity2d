using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Attack
    }
    private State state;
    [SerializeField]
    private float moveSpeed = 7f;
    private float directionX;
    private bool isFacingRight = true;

    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private BoxCollider2D boxcollider2D;
 
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float jumpHeight = 5;
    private bool isJumping = false;
    private bool isDoubleJump = false;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxcollider2D = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        state = State.Idle;
    }
    
    private void Update()
    {


        GetInput();

        ChangeDirection();
        UpdateAnimation();
        //Debug.Log("IsGround ? " + IsGround());
        AttackCombo();
        Jumping();
        Debug.Log("state : " + state);
    }

    private void FixedUpdate()
    {     
        Moving();
    }
    private void GetInput() 
    {
        directionX = Input.GetAxisRaw("Horizontal");
    }

    private void ChangeDirection()
    {
        if (directionX > 0) // nhan vat quay sang phai
        {
            //renderer.flipX = false;
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (directionX < 0) // nhan vat quay sang trai
        {
            //renderer.flipX = true;
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void UpdateAnimation()
    {
        if (state == State.Attack) return;
        if (directionX != 0 ) 
        {
            state = State.Running;
        }
        else 
        {
            state = State.Idle;
        } 
           
        anim.SetInteger("State", (int)state);
    }
    private void Moving()
    {
        
        rb2d.velocity = new Vector2(directionX * moveSpeed, rb2d.velocity.y);
        if(state== State.Attack) { rb2d.velocity = Vector2.zero; }
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }


    private void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if (IsGround() && !isJumping)
        {
            isDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGround() || isDoubleJump)
            {
                //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
                rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                isDoubleJump = !isDoubleJump;
                
            }

        }
        if (rb2d.velocity.y >= 0)
        {
            rb2d.gravityScale = 2f;
        }
        else if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = 10f;
        }
    }
    [SerializeField]
    private int combo = 0;
    private bool isAttacking = false;

    private void AttackCombo()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            state = State.Attack;
            anim.SetTrigger("combo" + combo);
        }
    }
    public void StartCombo()
    {
        isAttacking = false;
        state= State.Attack;
        if(combo < 2)
        {
            combo++;
        }
    }
    public void FinishAtkAnim() //finish
    {
        isAttacking= false;
        combo = 0;
        state = State.Idle;
    }

    [SerializeField]
    private LayerMask jumpAbleGround;
    private bool IsGround()
    {
        //Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (capsuleCollider2D.bounds.size.y / 2f + 0.1f), Color.red);
        //return Physics2D.BoxCast(capsuleCollider2D.bounds.center,
        // capsuleCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
        Debug.DrawRay(boxcollider2D.bounds.center, Vector2.down* (boxcollider2D.bounds.size.y/2f+ 0.1f), Color.red);
        return Physics2D.BoxCast(boxcollider2D.bounds.center,
        boxcollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
    }

}

