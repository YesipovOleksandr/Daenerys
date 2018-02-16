using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharState
{
    stoping,
    run,
    atack
}
public class PlayerController : MonoBehaviour {
    Vector3 position;
    public float speed = 3.0F;
    public float jump = 5.0F;


    //проверка на земеле или нет 
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;

    //анимация
    private Animator animator;

    //огненный шар
    FireBallController FireBall;


    void Start () {
        animator = GetComponent<Animator>();
  
    }
    private void Awake()
    {
        FireBall = Resources.Load<FireBallController>("FireBall");
    }

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void FixedUpdate() {
         State = CharState.stoping;
   
        CheckGround();
        if (Convert.ToBoolean(CnInputManager.GetAxis("Horizontal")))Run();
        if (isGrounded&& CnInputManager.GetButtonDown("Jump"))Jump();
        if (CnInputManager.GetButtonDown("Attack")) Attack();

    }

    private void Run()
    {
        position = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f);
        transform.position += position * speed * Time.deltaTime;
        if (position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
            State = CharState.run;
        
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * jump, ForceMode2D.Impulse);
    }


    private void Attack()
    {
        State = CharState.atack;
        Vector3 position = transform.position;
        position.y += 1F;
        FireBallController newFire=Instantiate(FireBall, position, FireBall.transform.rotation) as FireBallController;
        newFire.Direction = newFire.transform.right * position.x*2;

    }

    public void CheckGround()
    {
      //GameObject ground = GetComponentInChildren<GameObject>();
      isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,0.3F, whatIsGround);

    }




}
