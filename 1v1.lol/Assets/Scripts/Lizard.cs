using System.Runtime.CompilerServices;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    [SerializeField] GameObject fireball;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] GameObject selectArrow;
    GameObject o;
    Rigidbody2D rb;
    Animator animator;
    public bool controlling, direction, shoot;
    int state;
    float fireballSpeed, idleTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controlling = false;
        direction = true;
        o = new GameObject();
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
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Shoot();
            }
        }
        else
        {
            idleTime += Time.deltaTime;
            if (!direction && transform.position.x < maxX)
            {
                if (!shoot && idleTime <= 2f)
                {
                    state = 1;
                    GetComponent<SpriteRenderer>().flipX = true;
                    rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
                }
                else if (!shoot && idleTime != 0)
                {
                    rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
                    Shoot();
                }

            }
            else if (direction && transform.position.x > minX)
            {
                if (!shoot && idleTime <= 2f)
                {
                    state = 1;
                    GetComponent<SpriteRenderer>().flipX = false;
                    rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
                }
                else if (!shoot && idleTime != 0)
                {
                    rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
                    Shoot();
                }
            }
            else
            {
                direction = !direction;
            }
        }
        animator.SetInteger("State", state);
    }
    public void DelayFireball()
    {
        this.o.GetComponent<Rigidbody2D>().AddForceX(fireballSpeed, ForceMode2D.Impulse);
    }
    public void OnMouseDown()
    {
        //if (enemy == 0) manager.GetComponent<TullyMonster67>().FlyingSelect();
        //else if (enemy == 1) manager.GetComponent<TullyMonster67>().SkeletonSelect();
        //else manager.GetComponent<TullyMonster67>().LizardSelect();
        manager.GetComponent<TullyMonster67>().DeselectAll();
        controlling = true;
    }
    public void Select()
    {
        manager.GetComponent<TullyMonster67>().DeselectAll();
        controlling = true;
    }
    public void Deselect()
    {
        controlling = false;
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetInteger("State", 0);
        idleTime = 0;
    }
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
        shoot = true;
        this.o = Instantiate(fireball, transform.position, Quaternion.identity);
        if (direction)
        {
            fireballSpeed = Random.Range(-5f, -1f);//-2.5f;
            o.transform.position = new Vector2(transform.position.x - .4f, transform.position.y);
        }
        else
        {
            fireballSpeed = Random.Range(1f, 5f);//2.5f;
            o.transform.position = new Vector2(transform.position.x + .4f, transform.position.y);
        }
        o.GetComponent<Fireball>().enabled = true;
        Invoke("DelayFireball", .25f);
        idleTime = 0;
        Invoke("ShootDelay", .75f);
    }
    public void ShootDelay()
    {
        shoot = false;
    }
    private void OnMouseOver()
    {
        selectArrow.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnMouseExit()
    {
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
    }
}
