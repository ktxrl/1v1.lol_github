using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Wpoint : MonoBehaviour
{
    [SerializeField] List<GameObject> list = new List<GameObject>();
    [SerializeField] int currentWP = 0;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, list[currentWP].transform.position, speed * Time.deltaTime);
        transform.position = newPos;

        if (Mathf.Abs(transform.position.x - list[currentWP].transform.position.x) < .1 && Mathf.Abs(transform.position.y - list[currentWP].transform.position.y) < .1)
            currentWP = (currentWP + 1) % list.Count;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.tag.Equals("shield"))
        {

            gameObject.SetActive(false);
        }

    }
}
