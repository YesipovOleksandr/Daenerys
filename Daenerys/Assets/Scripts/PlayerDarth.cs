using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DarthState
{
    stoping,
    run,
    atack


}
public class PlayerDarth : MonoBehaviour
{
  
    // public int lifes = 3;
    // public int coin = 0;

    public float speed = 3.0F;
    public float jump = 5.0F;

    public Health health;

    public AudioSource KichAudio;

    //проверка на земеле или нет 
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded = false;

    //анимация
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;


    //с какой стороны бьет яйцом
    //private GameObject CircleColaiderRights;
    //private GameObject CircleColaiderLeft;
    public CircleCollider2D CircleColaiderRights;
    public CircleCollider2D CircleColaiderLeft;
    ////огненный шар
    //FireBallController FireBall;
    ////время между выстрелами 
    //public float shotsTime;
    //private float shotsTimeCounter;


    private DarthState State
    {
        get { return (DarthState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //shotsTimeCounter = 0;
 
      
    }

    private void Awake()
    {
        //CircleColaiderRights = GameObject.Find("CircleColaiderRights");
        //CircleColaiderLeft = GameObject.Find("CircleColaiderLeft");
        //FireBall = Resources.Load<FireBallController>("FireBall");   
    }

    void FixedUpdate()
    {

       State = DarthState.stoping;


        CheckGround();

        if (Convert.ToBoolean(CnInputManager.GetAxis("Horizontal"))) Run();
        if (isGrounded && CnInputManager.GetButtonDown("Jump")) Jump();
        if (CnInputManager.GetButtonDown("Attack")) Shoot();

        ////чтобы часто не стрелять
        //shotsTimeCounter -= Time.deltaTime;




        //фича высоко не прыгать
        if (gameObject.transform.position.y >= 5.500000F)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 5.500000F);
        }
    }

    private void Run()
    {
        // if (isGrounded)
        State = DarthState.run;
        Vector3 position = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f);
        transform.position += position * speed * Time.deltaTime;
        mySpriteRenderer.flipX = position.x < 0.0F;

      
    }

    private void Jump()
    {
    
        
            GetComponent<Rigidbody2D>().velocity = new Vector2(mySpriteRenderer.flipX ? -2.0F : 2.0F, jump);
        

    }

    //стрельба
    private void Shoot()
    {
        //if (shotsTimeCounter <= 0)
        //{      
        //Vector3 position = transform.position;
        //position.y += 1.5F;
        //FireBallController newFire = Instantiate(FireBall, position, FireBall.transform.rotation) as FireBallController;
        //newFire.Direction = newFire.transform.right * (mySpriteRenderer.flipX ? -1.0F : 1.0F);
        //shotsTimeCounter = shotsTime;
        //}


        if (mySpriteRenderer.flipX == true)
        {
            //CircleColaiderLeft.GetComponent<CircleCollider2D>().enabled = true;
            CircleColaiderLeft.enabled = true;
            Invoke("OfCircleColaiderLeft", 1);
        }
        else
        {
            CircleColaiderRights.enabled = true;
            //CircleColaiderRights.GetComponent<CircleCollider2D>().enabled = true;
            Invoke("OfCircleColaiderRights", 1);
        }

        KichAudio.Play();
        State = DarthState.atack;
     

    }

    public void OfCircleColaiderLeft()
    {
        CircleColaiderLeft.enabled = false;
        //CircleColaiderLeft.GetComponent<CircleCollider2D>().enabled = false;
    }
    public void OfCircleColaiderRights()
    {
        CircleColaiderRights.enabled = false;
        //CircleColaiderRights.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FireBallController fireBall = other.GetComponent<FireBallController>();
        if (fireBall)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            health.TakeDamage(30);
        }

        BulletController bullet = other.GetComponent<BulletController>();
        if (bullet)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            health.TakeDamage(30);
        }
        if (other.tag == "Asteroid")
        {
            health.TakeDamage(30);
        }
        if (other.tag == "health")
        {
            health.AddHealth(25);
            Destroy(other.gameObject);
        }


    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(mySpriteRenderer.flipX ? 300 : -300, 300));
            health.TakeDamage(20);
        }

    }

    //проверка на землю
    public void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.4F, whatIsGround);
        
    }
}
