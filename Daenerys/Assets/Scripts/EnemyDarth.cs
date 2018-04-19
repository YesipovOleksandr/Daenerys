using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyStateDarth
{
    run,
    damage,
    attack

}

public class EnemyDarth : MonoBehaviour {


    public int lifes = 10;
    public float speed = 5.0F;
    private Animator animator;
    private Vector3 direction;
    private SpriteRenderer sprite;

    //время длительности анимации получения урона
    private float TimeDamageCount;
    private float DamageTime = 0.5F;
    //време до атаки
    private float attackTime=1F;

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
       
    }
    private EnemyStateDarth State
    {
        get { return (EnemyStateDarth)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected void Start()
    {
        TimeDamageCount = 0;
        animator = GetComponent<Animator>();
        direction = transform.right;
        //Атака
        // InvokeRepeating("Hit", 0F, 3F);
        StartCoroutine(TestCoroutine());

    }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2F);
            attackTime = 1F;
          
        }
    }

    protected void FixedUpdate()
    {
       
      
        if (TimeDamageCount <= 0)
        {
            State = EnemyStateDarth.run;
        }
        else
        {
            State = EnemyStateDarth.damage;
        }

        if (State == EnemyStateDarth.run)
        {
            attackTime -= Time.deltaTime;
        }
        if (attackTime <= 0)
        {
            State = EnemyStateDarth.attack;
        }

        Move();
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
            direction *= -1.0F;
        if (other.tag == "Player")
        {
            lifes--;
            TimeDamageCount = DamageTime;
            //Destroy(other.gameObject);
            if (lifes <= 0)
            {
                Destroy(gameObject, DamageTime);
                
                SpecialEffectsHelper.Instance.Explosion(transform.position);

            }
        }

    }





    private void Move()
    {
        if (TimeDamageCount <= 0)
        {
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 1.0F + transform.right * direction.x * 2.0F, 0.1F);     
            // if (colliders.Length > 0) direction *= -1.0F;
            sprite.flipX = direction.x > 0.0F;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
        TimeDamageCount -= Time.deltaTime;
    }


  //  private void Hit()
    //{
      
    //    if (sprite.flipX)
    //    {

    //        CircleColaiderLeft.enabled = true;
    //        CircleColaiderRights.enabled = false;
    //    }
    //    else
    //    {
    //        CircleColaiderRights.enabled = true;
    //        CircleColaiderLeft.enabled = false;
    //    }
        
  //  }
}
