using UnityEngine;

public class Lizard : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    [SerializeField] GameObject fireball;
    public bool controlling, direction;
    int state;
    float fireballSpeed;
    GameObject o;
    Rigidbody2D rb;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controlling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling) // state 0: idle, 1: move, 2: shoot
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
                state = 1;
                direction = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
                state = 1;
                direction = false;
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
                state = 0;
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                state = 2;
                //initialize fireball
                o = Instantiate(fireball, transform.position, Quaternion.identity);
                if (direction)
                {
                    fireballSpeed = -2.5f;
                    o.transform.position = new Vector2(transform.position.x - .4f, transform.position.y);
                }
                else
                {
                    fireballSpeed = 2.5f;
                    o.transform.position = new Vector2(transform.position.x + .4f, transform.position.y);
                }
                o.GetComponent<Fireball>().enabled = true;
                Invoke("DelayFireball", .25f);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        animator.SetInteger("State", state);
    }
    public void DelayFireball()
    {
        o.GetComponent<Rigidbody2D>().AddForceX(fireballSpeed, ForceMode2D.Impulse);
    }
    public void OnMouseDown()
    {
        controlling = true;
        if (enemy == 0) manager.GetComponent<TullyMonster67>().FlyingSelect();
        else if (enemy == 1) manager.GetComponent<TullyMonster67>().SkeletonSelect();
        else manager.GetComponent<TullyMonster67>().LizardSelect();
    }
    public void Deselect()
    {
        controlling = false;
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetInteger("State", 0);
    }
}
