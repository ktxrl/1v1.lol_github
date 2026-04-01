using UnityEngine;

public class slidercode : MonoBehaviour
{
    public bool MV = false;
    public bool SE = false;
    public bool MP = false;
    public int sixsevem = 0;
    public bool MVmove = false;
    public bool SEmove = false;
    public bool MPmove = false;
    public bool single = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (MV && MVmove)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
                single = true;
            }
            if (transform.position.y > 1.52 || transform.position.y < 1.52)
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
        if (SE && SEmove)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
                single = true;
            }
            if (transform.position.y > -1.23 || transform.position.y < -1.23)
            {
                transform.position = new Vector2(transform.position.x, -1.23f);
            }
            if (transform.position.x > 6)
            {
                transform.position = new Vector2(6f, -1.23f);
            }
            if (transform.position.x < -8)
            {
                transform.position = new Vector2(-8f, -1.23f);
            }
        }
        if (MP && MPmove)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
                single = true;
            }
            if (transform.position.y > -3.98 || transform.position.y < -3.98)
            {
                transform.position = new Vector2(transform.position.x, -3.98f);
            }
            if (transform.position.x > 6)
            {
                transform.position = new Vector2(6f, -3.98f);
            }
            if (transform.position.x < -8)
            {
                transform.position = new Vector2(-8f, -3.98f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            single = false;
        }
        if (Input.GetMouseButton(0) == false)
        {
            single = false;
        }



    }
    private void OnMouseOver()
    {
        if (single == true)
        {
            return;
        }
        if (MV )
        {

                MVmove = true;
            SEmove = false;
            MPmove = false;
            
        }
            
        if (SE )
             { 
            SEmove = true;
            MPmove = false;
            MVmove = false;
        } 
        if (MP )
        {
           
                MPmove = true;
            SEmove = false;
            MVmove = false;
            
        }
    }
    private void OnMouseDown()
    {
        
    }

}
