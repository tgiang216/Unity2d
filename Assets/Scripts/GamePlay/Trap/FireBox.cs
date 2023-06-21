using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour , ITrap
{
    [SerializeField]
    private float timeOn;
    [SerializeField]
    private float timeOff;
    [SerializeField]
    bool isTurningOn;
    [SerializeField]
    float damage = 50f;

    private float timer;
    [SerializeField]
    Animator animator;

    private void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(isTurningOn)
        {
            if(timer > timeOn)
            {
                isTurningOn = false;
                animator.Play("FireOff");
                this.gameObject.tag = "Wall";
                timer = 0;
                
            }
        }else
        {
            if(timer > timeOff)
            {
                isTurningOn = true;
                animator.Play("Fire");
                this.gameObject.tag = "Trap";
                timer = 0;
                
            }
        }
    }

    public float MakeDamage()
    {
        return damage;
    }
}
