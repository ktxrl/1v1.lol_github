using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] List<GameObject> coins;
    [SerializeField] Text timeText;
    [SerializeField] Text coinText;
    [SerializeField] Text dieText;
    [SerializeField] Text lifeText;
    Rigidbody2D rb;
    Animator animator;
    bool left, right, roll, die, direction; //false = left, true = right
    float time, rolltime, rollcooldown, ogX, ogY;
    int coinCount, lives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() // 0 = idle, 1 = running, 2 = jumping, 3 = falling, 4 = duck
    {
        transform.position = new Vector2(-7.5f, -1.6f);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        time = 0;
        rolltime = 0;
        rollcooldown = 0;
        ogX = transform.position.x;
        ogY = transform.position.y;
        die = false;
        direction = true;
        coinCount = 0;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (die) rb.linearVelocity = Vector2.zero;
        if (roll)
        {
            rolltime += Time.deltaTime;
            if (rolltime > .3f)
            {
                rolltime = 0;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                roll = false;
            }
        }
        else
        {
            rollcooldown += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && !roll) left = true;
        if (Input.GetKey(KeyCode.D) && !roll) right = true;
        if (Input.GetKeyUp(KeyCode.A)) left = false;
        if (Input.GetKeyUp(KeyCode.D)) right = false;
        if (Input.GetKey(KeyCode.Space) && !roll && rollcooldown > .2f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            roll = true;
            rollcooldown = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) && IsGround() && !roll && !die) rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        UpdateState();
        //if (die && Input.GetKeyDown(KeyCode.R))
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    dieText.enabled = false;
        //    transform.position = new Vector2(ogX, ogY);
        //    time = 0;
        //    die = false;
        //    gemsCount = 0;
        //    lives = 3;
        //    lifeText.text = "Lives: " + lives;
        //    door.GetComponent<Door>().Deactivate();
        //    for (int i = 0; i < gems.Count; i++)
        //    {
        //        gems[i].SetActive(true);
        //    }
        //}
    }
    public void UpdateState()
    {
        int state;
        if (roll) state = 4;
        else if (rb.linearVelocityY <= 0 && !IsGround()) state = 3;
        else if (rb.linearVelocityY > 0 && !IsGround()) state = 2;
        else if (IsGround() && Math.Abs(rb.linearVelocityX) > 0.1f) state = 1;
        else state = 0;
        if (die) state = 0;
        animator.SetInteger("State", state);
    }
    public void FixedUpdate()
    {
        if (!die)
        {
            float horizontal = 0f;
            if (roll)
            {
                if (direction)
                {
                    horizontal += 2.5f * speed;
                }
                else
                {
                    horizontal -= 2.5f * speed;
                }
            }
            else if (left)
            {
                horizontal -= speed;
                GetComponent<SpriteRenderer>().flipX = true;
                direction = false;
            }
            else if (right)
            {
                horizontal += speed;
                GetComponent<SpriteRenderer>().flipX = false;
                direction = true;
            }
            rb.linearVelocity = new Vector2(horizontal, rb.linearVelocityY);
        }
    }
    public bool IsGround()
    {
        RaycastHit2D[] rays1 = Physics2D.RaycastAll(new Vector2(transform.position.x - .4f, transform.position.y), Vector2.down, .8f);
        RaycastHit2D[] rays2 = Physics2D.RaycastAll(new Vector2(transform.position.x + .4f, transform.position.y), Vector2.down, .8f);
        RaycastHit2D[] rays3 = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), Vector2.down, .8f);
        if (rays1.Length > 1 || rays2.Length > 1 || rays3.Length > 1) return true;
        return false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            collision.gameObject.SetActive(false);
            coinCount++;
        }
        else if (collision.gameObject.tag == "door")
        {
            //win
            //collision.gameObject.GetComponent<Door>().Activate();
            //die = true;
            //rb.linearVelocity = Vector2.zero;
            //dieText.enabled = true;
            //dieText.text = "You Won! You collected " + gemsCount + " gems";
            //UpdateState();
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (roll) rolltime = .5f;
        if (collision.gameObject.tag == "spike")
        {
            //die
            lives--;
            lifeText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                die = true;
                rb.linearVelocity = Vector2.zero;
                dieText.enabled = true;
                dieText.text = "You Died. You collected " + coinCount + " gems";
                UpdateState();
            }
            else
            {
                transform.position = new Vector2(ogX, ogY);
            }
        }
        else if (collision.gameObject.tag == "enemy")
        {
            lives--;
            lifeText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                die = true;
                rb.linearVelocity = Vector2.zero;
                dieText.enabled = true;
                dieText.text = "You Died. You collected " + coinCount + " gems";
                UpdateState();
            }
            else
            {
                transform.position = new Vector2(ogX, ogY);
            }
            
        }
    }
}
 