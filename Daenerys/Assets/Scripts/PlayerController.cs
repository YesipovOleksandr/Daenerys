using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharState
{
    stoping,
    dance,
    run,
    atack,
    jump

}
public class PlayerController : MonoBehaviour {

    // public int lifes = 3;
    // public int coin = 0;
    //музыка
    public AudioSource KichAudio;
    public AudioSource KichAudio2;

    public float speed = 3.0F;
    public float jump = 5.0F;

    public Health health;

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

    //время между танцами 
    private float danceTime;
    //время между ударами
    private float attackcd;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Start () {
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //shotsTimeCounter = 0;
        danceTime = 3F;
        attackcd = 0.5F;
    }
 
    private void Awake()
    {
        //KichAudio = GetComponent<AudioSource>();
        //KichAudio2 = GetComponent<AudioSource>();
        //CircleColaiderRights =GameObject.Find("CircleColaiderRights");
        //CircleColaiderLeft = GameObject.Find("CircleColaiderLeft");
        //FireBall = Resources.Load<FireBallController>("FireBall");   
    }

    void FixedUpdate() {

       
        //анимация бездействия не слишком часто
        State = CharState.stoping;

        if (State == CharState.stoping)
        {
            danceTime -= Time.deltaTime;
        }
        if (danceTime <= 0)
        {
            State = CharState.dance;

        }

        CheckGround();

        if (Convert.ToBoolean(CnInputManager.GetAxis("Horizontal")))Run();
        if (isGrounded&& CnInputManager.GetButtonDown("Jump"))Jump();
        if (CnInputManager.GetButtonDown("Attack")) Shoot();

        ////чтобы часто не стрелять
        //shotsTimeCounter -= Time.deltaTime;

     


        //фича высоко не прыгать
        if (gameObject.transform.position.y >= 5.500000F)
        {
            gameObject.transform.position =new Vector2(gameObject.transform.position.x, 5.500000F );
        }
    }

    private void Run()
    {
        if (isGrounded) State = CharState.run;
        Vector3 position = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f);
        transform.position += position * speed * Time.deltaTime;
        mySpriteRenderer.flipX = position.x < 0.0F;

        danceTime = 3;
    }

    private void Jump()
    {
        //если ты высоко -прыгай ниже
        //if (transform.position.y >= 0.5F)
        //{
           
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(mySpriteRenderer.flipX ? -2.0F : 2.0F, jump/2);
        //}
        //else
        //{
            GetComponent<Rigidbody2D>().velocity = new Vector2(mySpriteRenderer.flipX ? -2.0F : 2.0F, jump);
      //  }
       
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
        var random = UnityEngine.Random.Range(0, 2);
        if (random == 1)
        {
            KichAudio.Play();
        }
        else
        {
            KichAudio2.Play();
        }

        State = CharState.atack;
        danceTime = 3;
    
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
        if (other.tag== "health")
        {
            health.AddHealth(25);
            Destroy(other.gameObject);
        }

        
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {         
            GetComponent<Rigidbody2D>().AddForce(new Vector2(mySpriteRenderer.flipX ? 300 : -300,300));
            health.TakeDamage(20);
        }

        if (coll.gameObject.tag == "Darth")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(mySpriteRenderer.flipX ? 600 : -600, 400));
            health.TakeDamage(30);
        }

    }

    //проверка на землю
    public void CheckGround()
    {
      isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,0.3F, whatIsGround);
        if (!isGrounded) State = CharState.jump;
    }
}
