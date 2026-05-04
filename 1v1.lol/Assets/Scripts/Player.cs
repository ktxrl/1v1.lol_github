using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour // double jump uses stamina, add powerups (for stamina?),
                                    // fireboy watergirl lever/door
                                    // spikes
                                    // adding tab to select through monsters,
                                    // giving enemy powerups
{
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] List<GameObject> coins;
    [SerializeField] Text timeText;
    [SerializeField] Text coinText;
    [SerializeField] Text dieText;
    [SerializeField] Text lifeText;
    [SerializeField] GameObject comboBar;
    [SerializeField] Image staminaBar;
    [SerializeField] Image healthBar;
    [SerializeField] float stamina;
    [SerializeField] float maxStamina;
    [SerializeField] float runCost;
    [SerializeField] float attackCost;
    [SerializeField] float rollCost;
    [SerializeField] float jumpCost;
    [SerializeField] float shieldCost;
    [SerializeField] float chargeRate;
    //[SerializeField] float health;
    //[SerializeField] float maxHealth;
    [SerializeField] GameObject life1;
    [SerializeField] GameObject life2;
    [SerializeField] GameObject life3;

    [SerializeField] AudioSource slash1;
    [SerializeField] AudioSource slash2;

    private Coroutine recharge;
    private bool isOpening = false;
    bool shielding = false;
    

    Rigidbody2D rb;
    Animator animator;
    bool left, right, roll, die, direction, attack, doubleJump; //false = left, true = right
    float time, rolltime, rollcooldown, ogX, ogY, lastAttack, comboDelay, attackTime, attackCooldown;
    int coinCount, lives, attackIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() // 0 = idle, 1 = running, 2 = jumping, 3 = falling, 4 = roll, 5 = walk
    { // C = roll, V = attack
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        animator = GetComponent<Animator>();
        time = 0;
        rolltime = 0;
        rollcooldown = 0;
        ogX = transform.position.x;
        ogY = transform.position.y;
        die = false;
        direction = true;
        doubleJump = true;
        coinCount = 0;
        lives = 3;
        comboDelay = 1f;
        attackTime = 0;
        attackIndex = 0;
        attackCooldown = 0;
        staminaBar.fillAmount = maxStamina;
        //healthBar.fillAmount = maxHealth;
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        //door = GameObject.Find("gate");
    }

    // Update is called once per frame
    void Update()
    {
        if (die) rb.linearVelocity = Vector2.zero;
        if (roll)
        {
            rolltime += Time.deltaTime;
            if (rolltime > .5f)
            {
                rolltime = 0;
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina >= runCost * Time.deltaTime)
            {
                speed = 4f;

                stamina -= runCost * Time.deltaTime;
                if (stamina < 0) stamina = 0;
                staminaBar.fillAmount = stamina / maxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
            else
            {
                //signal unable to run
                speed = 2.5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 2.5f;
        if (Input.GetKey(KeyCode.C) && !roll && rollcooldown > .2f && IsGround())
        {
            if (stamina >= rollCost)
            {
                roll = true;
                rollcooldown = 0;

                stamina -= rollCost;
                if (stamina < 0) stamina = 0;
                staminaBar.fillAmount = stamina / maxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
            else
            {
                //signal unable to roll
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && !roll && !die)
        {
            if (IsGround())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                doubleJump = false;
            }
            else if (stamina >= jumpCost && !doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce/1.3f);
                doubleJump = true;

                stamina -= jumpCost;
                if (stamina < 0) stamina = 0;
                staminaBar.fillAmount = stamina / maxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
        }
        UpdateState();
        if (Input.GetKey(KeyCode.Space))
        {
            if (stamina >= shieldCost)
            {
                animator.SetBool("Shield", true);
                shielding = true;
                stamina -= shieldCost * Time.deltaTime;
                if (stamina < 0) stamina = 0;
                staminaBar.fillAmount = stamina / maxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Shield", false);
            shielding = false;
        }
        Debug.Log(attackIndex);
        if (Time.time - lastAttack > comboDelay) attackIndex = 0;
        if (attack)
        {
            attackTime += Time.deltaTime;
            if (attackTime > 1f)
            {
                attack = false;
                attackTime = 0;
            }
        }
        attackCooldown += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.V) && !roll && !die && attackCooldown > .5f)
        {
            if (stamina >= attackCost)
            {
                attack = true;
                lastAttack = Time.time;
                animator.SetTrigger("Attack");
                animator.SetInteger("AttackIndex", attackIndex);
                comboBar.GetComponent<AttackBar>().ResetCombo();
                if (attackIndex == 2) slash2.Play();
                else slash1.Play();
                attackIndex++;
                if (attackIndex > 2) attackIndex = 0;
                attackCooldown = 0;

                stamina -= attackCost;
                if (stamina < 0) stamina = 0;
                staminaBar.fillAmount = stamina / maxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
            else
            {
                //signal unable to attack
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //block
        }
        if (isOpening)
        {
            Vector2 targetPosition = new Vector2(GameObject.Find("gate").transform.position.x, -0.33f);
            float speed = 3f * Time.deltaTime;

            GameObject.Find("gate").transform.position = Vector2.MoveTowards(GameObject.Find("gate").transform.position, targetPosition, speed);

            /*if (Vector2.Distance(door.transform.position, targetPosition) < 0.01f)
            {
                isOpening = false;
            }*/
        }
        //if (die && Input.GetKeyDown(KeyCode.R))
        if (Input.GetKeyDown(KeyCode.R))
        {
            dieText.enabled = false;
            transform.position = new Vector2(ogX, ogY);
            time = 0;
            die = false;
            lives = 3;
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
    }
    public void UpdateState()
    {
        int state;
        if (roll) state = 4;
        else if (rb.linearVelocityY <= 0 && !IsGround()) state = 3;
        else if (rb.linearVelocityY > 0 && !IsGround()) state = 2;
        else if (IsGround() && Math.Abs(rb.linearVelocityX) > 0.1f)
        {
            if (speed == 3f) state = 1;
            else state = 5;
        }
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
                    horizontal += 1.5f * speed;
                }
                else
                {
                    horizontal -= 1.5f * speed;
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
        RaycastHit2D[] rays1 = Physics2D.RaycastAll(new Vector2(transform.position.x - .46f, transform.position.y + .06f), Vector2.down, .1f);
        RaycastHit2D[] rays2 = Physics2D.RaycastAll(new Vector2(transform.position.x + .34f, transform.position.y +.06f), Vector2.down, .1f);
        RaycastHit2D[] rays3 = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + .06f), Vector2.down, .1f);
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
        else if (collision.gameObject.tag == "fireball")
        {
            if (shielding)
            {
                Destroy(collision.gameObject);
                return;
            }
            else
            {
                animator.SetTrigger("Hurt");
                lives--;
                if (lives == 2)
                {
                    life3.SetActive(false);
                }
                if (lives == 1)
                {
                    life3.SetActive(false);
                    life2.SetActive(false);
                }
                if (lives <= 0)
                {
                    life3.SetActive(false);
                    life2.SetActive(false);
                    life1.SetActive(false);
                    die = true;
                    rb.linearVelocity = Vector2.zero;
                    dieText.enabled = true;
                    dieText.text = "You Died. You collected " + coinCount + " gems";
                    UpdateState();
                }
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "jump")
        {
            doubleJump = false;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "speed")
        {
            if (speed <= 3)
            {
                speed *= 2;
                Invoke("Speed", 1f);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "health")
        {
            if (lives < 3) lives++;
        }
        else if (collision.gameObject.tag == "fan")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 19f);
        }
        else if (collision.gameObject.tag == "fan2")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 15f);
        }
        if (collision.gameObject.tag == "button for door")
        {
            isOpening = true;
            
        }
    }
    public void Speed()
    {
        speed /= 2;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            //die
            //lives--;
            //lifeText.text = "Lives: " + lives;
            lives--;
            if (lives == 2)
            {
                life3.SetActive(false);
            }
            if (lives == 1)
            {
                life3.SetActive(false);
                life2.SetActive(false);
            }
            if (lives <= 0)
            {
                life3.SetActive(false);
                life2.SetActive(false);
                life1.SetActive(false);
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
            //lives--;
            //lifeText.text = "Lives: " + lives;
            //if (lives <= 0)
            //{
            //    die = true;
            //    rb.linearVelocity = Vector2.zero;
            //    dieText.enabled = true;
            //    dieText.text = "You Died. You collected " + coinCount + " gems";
            //    UpdateState();
            //}
            //else
            //{\
            if (shielding) return;
            lives--;
            if (lives == 2)
            {
                life3.SetActive(false);
            }
            if (lives == 1)
            {
                life3.SetActive(false);
                life2.SetActive(false);
            }
            if (lives <= 0)
            {
                life3.SetActive(false);
                life2.SetActive(false);
                life1.SetActive(false);
                die = true;
                rb.linearVelocity = Vector2.zero;
                dieText.enabled = true;
                dieText.text = "You Died. You collected " + coinCount + " gems";
                UpdateState();
            }
            animator.SetTrigger("Hurt");

            //health -= 20;
            //if (health < 0) health = 0;
            //healthBar.fillAmount = health / maxHealth;
            //transform.position = new Vector2(ogX, ogY);
            //}
        }
        
    }
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1.5f);
        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.01f);
        }
    }
}