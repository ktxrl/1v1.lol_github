using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1buttom : MonoBehaviour
{
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
        SceneManager.LoadScene("map 1");

    }
}
