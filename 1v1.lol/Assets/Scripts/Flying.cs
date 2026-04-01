using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] float speed;
    [SerializeField] int enemy; //0 = flying, 1 = skeleton, 2 = lizard
    public bool controlling, direction;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controlling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            } 
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            }
        }
        else
        {

        }
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
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }
}
