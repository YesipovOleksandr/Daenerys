using UnityEngine;
using System.Collections;
using System.Linq;

public class MoveableMonster : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
    private Animator animator;
    private Vector3 direction;
    
    public GameObject sp;

    private SpriteRenderer sprite;

    protected  void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected  void Start()
    {
        animator = GetComponent<Animator>();
        direction = transform.right;
    }

    protected void Update()
    {
        Move();
    }

    protected  void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        if (playerController)
        {
            if (Mathf.Abs(playerController.transform.position.x - transform.position.x) < 0.5F);
           
        }
    }

    private void Move()
    {
        State = CharState.run;
        sp.transform.position= transform.position+ transform.up * 3.0F + transform.right * direction.x * 2.0F;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position+transform.up*1.0F+transform.right*direction.x*2.0F,0.1F);


        if (colliders.Length > 0)direction*=-1.0F;
        sprite.flipX = direction.x > 0.0F;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
