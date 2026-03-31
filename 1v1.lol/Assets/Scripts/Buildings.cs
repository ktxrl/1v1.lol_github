using UnityEngine;

public class Buildings : MonoBehaviour
{
    private SpriteRenderer renderer;
    [SerializeField] float xspeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.mainTextureOffset += new Vector2(xspeed * Time.deltaTime, 0);
    }
}
