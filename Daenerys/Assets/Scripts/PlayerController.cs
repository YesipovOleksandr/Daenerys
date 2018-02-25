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

    private Boolean side;

    //проверка на земеле или нет 
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;

    //анимация
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;

    //огненный шар
    FireBallController FireBall;


    void Start () {
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

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
        mySpriteRenderer.flipX = position.x < 0.0F;    
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
        position.y += 1.5F;
        //position.z = -1.5F;
        FireBallController newFire = Instantiate(FireBall, position, FireBall.transform.rotation) as FireBallController;
        newFire.Direction = newFire.transform.right*(mySpriteRenderer.flipX ? -1.0F : 1.0F);
                 

    }

    public void CheckGround()
    {
      //GameObject ground = GetComponentInChildren<GameObject>();
      isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,0.3F, whatIsGround);

    }




}
