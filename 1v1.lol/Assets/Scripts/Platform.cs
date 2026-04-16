using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] List<GameObject> list = new List<GameObject>();
    [SerializeField] int currentWP;
    [SerializeField] float speed;
    [SerializeField] GameObject manager;
    [SerializeField] int index;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    bool controlling;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWP = 0;
        controlling = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlling)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX) 
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX)
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, 0);
            }
        }
        else
        {
            Vector2 goal = list[currentWP].transform.position;
            Vector2 newPos = Vector2.MoveTowards(transform.position, list[currentWP].transform.position, speed * Time.deltaTime);
            transform.position = newPos;
            if (Mathf.Abs(transform.position.x - goal.x) < .1 && Mathf.Abs(transform.position.y - goal.y) < .1)
                currentWP = (currentWP + 1) % list.Count;
        }
    }
    public void Deselect()
    {
        controlling = false;
    }
    public void OnMouseDown()
    {
        manager.GetComponent<TullyMonster67>().DeselectAll();
        controlling = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" || collision.gameObject.tag == "enemy") 
            collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" || collision.gameObject.tag == "enemy") 
            collision.transform.SetParent(null);
    }
}
