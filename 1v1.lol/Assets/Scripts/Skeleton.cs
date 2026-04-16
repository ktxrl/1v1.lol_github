using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    [SerializeField] float minX;
    [SerializeField] float maxX;
    public bool controlling, direction;
    Rigidbody2D rb;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controlling = false;
        direction = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = true;
                GetComponent<SpriteRenderer>().flipX = true;
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
                animator.SetInteger("State", 1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = false;
                GetComponent<SpriteRenderer>().flipX = false;
                rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
                animator.SetInteger("State", 1);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
                animator.SetInteger("State", 0);
            }
        }
        else
        {
            animator.SetInteger("State", 1);
            if (!direction && transform.position.x < maxX)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            }
            else if (direction && transform.position.x > minX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            }
            else
            {
                direction = !direction;
            }
        }
    }
    public void OnMouseDown()
    {
        //controlling = true;
        //if (enemy == 0) manager.GetComponent<TullyMonster67>().FlyingSelect();
        //else if (enemy == 1) manager.GetComponent<TullyMonster67>().SkeletonSelect();
        //else manager.GetComponent<TullyMonster67>().LizardSelect();
        manager.GetComponent<TullyMonster67>().DeselectAll();
        controlling = true;
    }
    public void Deselect()
    {
        controlling = false;
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetInteger("State", 0);
    }
}
