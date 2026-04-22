using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] GameObject selectArrow;
    public bool controlling, direction;
    Rigidbody2D rb;
    Animator animator;

    void Start() //Easy Enemy Health Bars in Unity Youtube
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controlling = false;
        direction = false;
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
    }

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
            if (Input.GetKeyDown(KeyCode.RightControl)) Deselect();
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
        animator.SetInteger("State", 0);
        selectArrow.GetComponent<SpriteRenderer>().enabled = false;
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
}
