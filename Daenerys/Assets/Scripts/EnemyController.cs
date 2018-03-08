using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    run,
    damage

}
public class EnemyController : MonoBehaviour {

    Vector2 vel = new Vector2(-2f, 0);
    private int lifes = 3;
    private Animator animator;
    // Use this for initialization


    void Start()
    {
        State = EnemyState.run;
        animator = GetComponent<Animator>();

    }

    private EnemyState State
    {
        get { return (EnemyState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    void FixedUpdate()
    {
        //State = EnemyState.run;
        gameObject.GetComponentInChildren<Rigidbody2D>().velocity = vel;
    }




    private void OnTriggerStay2D(Collider2D other)
    {
        FireBallController fireBall = other.GetComponent<FireBallController>();
        if (fireBall)
        {
            lifes--;
            State = EnemyState.damage;
            Destroy(other.gameObject);
            if (lifes <= 0)
            {
                Destroy(gameObject);
            }
        
            print(lifes);
         
        }
    }

   
    
   

    

}
