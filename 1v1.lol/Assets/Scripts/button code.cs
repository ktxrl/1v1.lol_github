using UnityEngine;
using UnityEngine.SceneManagement;

public class buttoncode : MonoBehaviour
{
    public bool map1button;
    public bool settingsbutton;
    public bool startbutton;
    public bool settingsback;
    public bool selectback;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(map1button)
        {
            SceneManager.LoadScene("map 1");
        }
        if(settingsbutton)
        {
            SceneManager.LoadScene("Settings screen");
        }
        if(startbutton)
        {
            SceneManager.LoadScene("map select");
        }
        if (settingsback)
        {
            SceneManager.LoadScene("Start Screen");
        }
        if (selectback)
        {
            SceneManager.LoadScene("Start Screen");
        }



    }
}
