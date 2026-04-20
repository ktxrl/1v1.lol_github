using UnityEngine;

public class slidercode : MonoBehaviour
{
    public bool MV = false;
    public bool SE = false;
    public bool MP = false;
    [SerializeField] GameObject manager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        manager = GameObject.Find("Manager");
        if(MP)
        {
            float volume = (transform.position.x + 6) / 12;
            if (volume < 0) volume = 0;
            if (volume > 1)
            {
                volume = 1;
            }
            manager.GetComponent<TullyMonster67>().ChangeMusicVolume(volume);
        }
        if (MV)
        {
            float volume = (transform.position.x + 6) / 12;
            if (volume < 0) volume = 0;
            if (volume > 1)
            {
                volume = 1;
            }
            manager.GetComponent<TullyMonster67>().ChangeMainVolume(volume);
        }
        if (SE)
        {
            float volume = (transform.position.x + 6) / 12;
            if (volume < 0) volume = 0;
            if (volume > 1)
            {
                volume = 1;
            }
            manager.GetComponent<TullyMonster67>().ChangeSEVolume(volume);
        }









    }
    private void OnMouseOver()
    {
        if (MV)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
                
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
        if (SE)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
               
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
        if (MP)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(mousePos.x - 0.6f, mousePos.y - 0.5f);
                
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

    }

}
