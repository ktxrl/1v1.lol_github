using UnityEngine;
using UnityEngine.SceneManagement;

public class slidercode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
        }
        if(transform.position.y > 1.52 || transform.position.y < 1.52)
        {
            transform.position = new Vector2(transform.position.x, 1.52f);
        }
        if (transform.position.x > 6)
        {
            transform.position = new Vector2(6f, 1.52f);
        }
        if (transform.position.x < -8)
        {
            transform.position = new Vector2(-8f, 1.52f);
        }
    }
    
}
