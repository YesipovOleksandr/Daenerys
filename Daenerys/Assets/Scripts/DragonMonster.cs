using UnityEngine;

public enum DragonState
{
    run,
    damage

}

public class DragonMonster : MonoBehaviour
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
        FireBallController fireBall;
        //монетка
        GameObject coin;
        //жизни
        GameObject health;

        protected void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            fireBall = Resources.Load<FireBallController>("FireBall");
            coin = Resources.Load<GameObject>("Coin");
            health = Resources.Load<GameObject>("Health");
        }
        private DragonState State
        {
            get { return (DragonState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }
        protected void Start()
        {
            TimeDamageCount = 0;
            animator = GetComponent<Animator>();
            direction = transform.right;
            //стрельба 
            InvokeRepeating("Shoot", 1F, 3F);
        }

        protected void FixedUpdate()
        {
            if (TimeDamageCount <= 0)
            {
                State = DragonState.run;
            }
            else
            {
                State = DragonState.damage;
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
        position.y += 3.5F;
        position.x += 1F;
            FireBallController newFireBall = Instantiate(fireBall, position, fireBall.transform.rotation) as FireBallController;
            newFireBall.Direction = direction;
        }
    }
