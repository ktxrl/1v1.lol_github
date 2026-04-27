using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] GameObject selectArrow;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] HealthBar healthBar;
    public bool controlling, direction;
    Rigidbody2D rb;
    bool left, right, up, down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controlling = false;
        direction = true;
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Update()
    {
        if (controlling)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                left = true;
                //rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            } else left = false;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                right = true;
                //rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            } else right = false;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //rb.linearVelocity = new Vector2(rb.linearVelocityX, speed);
                up = true;
            }
            else up = false;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //rb.linearVelocity = new Vector2(rb.linearVelocityX, -speed);
                down = true;
            }
            else down = false;
            if (Input.GetKeyDown(KeyCode.RightControl)) Deselect();
        }
        else
        {
            if (!direction && transform.position.x < maxX)
            {
                right = true;
                left = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction && transform.position.x > minX)
            {
                left = true;
                right = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                direction = !direction;
            }
        }
    }
    public void FixedUpdate()
    {
        float xSpeed = 0;
        float ySpeed = 0;
        if (left) xSpeed = -speed;
        else if (right) xSpeed = speed;
        if (up) ySpeed = speed;
        else if (down) ySpeed = -speed;
        rb.linearVelocity = new Vector2(xSpeed, ySpeed);
    }
    public void OnMouseDown()
    {
        if (manager.GetComponent<TullyMonster67>().GetControl())
        {
            manager.GetComponent<TullyMonster67>().DeselectAll();
            controlling = true;
            manager.GetComponent<TullyMonster67>().ResetControl();
        }
    }
    public void Deselect()
    {
        controlling = false;
        rb.linearVelocity = new Vector2(0, 0);
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!controlling) direction = !direction;
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
