using UnityEngine;

public enum EnemyState
{
    run,
    damage

}
public class MoveableMonster : MonoBehaviour
{

    private int lifes = 3;
    public float speed = 5.0F;
    private Animator animator;
    private Vector3 direction;
    private SpriteRenderer sprite;

    //время длительности анимации получения урона
    private float TimeDamageCount;
    private float DamageTime = 0.5F;

    //Пуля
    BulletController bullet;
    //монетка
    GameObject coin;
    //жизни
    GameObject health;

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        bullet = Resources.Load<BulletController>("Bullet");
        coin = Resources.Load<GameObject>("Coin");
        health = Resources.Load<GameObject>("Health");
    }
    private EnemyState State
    {
        get { return (EnemyState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected void Start()
    {   
        TimeDamageCount = 0;
        animator = GetComponent<Animator>();
        direction = transform.right;
        //стрельба 
        InvokeRepeating("Shoot", 5F, 3F);
    }

    protected void FixedUpdate()
    {
        if (TimeDamageCount <= 0) {
            State = EnemyState.run;
        }
        else
        {
            State = EnemyState.damage;
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
                //рандомные деньги 
                var random = Random.Range(1, 5);
                for (var i = 0; i < random; i++)
                {
                    Instantiate(coin, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), -1F, 0F), Quaternion.identity);
                }
                //рандомные жизни
                if (random == 1 || random == 4)
                {
                    Instantiate(health, new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), -1F, 0F), Quaternion.identity);
                }

                SpecialEffectsHelper.Instance.Explosion(transform.position);

            }
        }

        }


    private void OnTriggerStay2D(Collider2D other)
    {
       
        
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


    private void Shoot()
    {
            Vector3 position = transform.position;
            position.y += 1.5F;
            BulletController newBullet = Instantiate(bullet, position, bullet.transform.rotation) as BulletController;
            newBullet.Direction = direction;         
    }
}
