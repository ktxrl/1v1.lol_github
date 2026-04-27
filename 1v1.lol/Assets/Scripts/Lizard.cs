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
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] HealthBar healthBar;
    GameObject o;
    Rigidbody2D rb;
    Animator animator;
    public bool controlling, direction, shoot;
    int state;
    float fireballSpeed, idleTime, shootCooldown;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controlling = false;
        direction = true;
        o = new GameObject();
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
        healthBar = GetComponentInChildren<HealthBar>();
    }

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
            if (Input.GetKeyDown(KeyCode.RightShift) && shootCooldown > 1f)
            {
                Shoot();
                shootCooldown = 0;
            }
            shootCooldown += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.RightControl)) Deselect();
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
        if (manager.GetComponent<TullyMonster67>().GetControl())
        {
            manager.GetComponent<TullyMonster67>().DeselectAll();
            controlling = true;
            shoot = false;
            manager.GetComponent<TullyMonster67>().ResetControl();
        }
    }
    public void Deselect()
    {
        controlling = false;
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetInteger("State", 0);
        idleTime = 0;
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
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
        if (!controlling)
            selectArrow.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "speed")
        {
            if (speed <= 1.5f)
            {
                speed *= 2;
                Invoke("Speed", 1f);
            }
        }
    }
    public void Speed()
    {
        speed /= 2;
    }
}
