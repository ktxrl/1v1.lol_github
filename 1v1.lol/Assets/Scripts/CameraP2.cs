using UnityEngine;

public class CameraP2 : MonoBehaviour
{
    public Vector3 dragOrigin;
    public bool dragging = false;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float minSize = 2f;
    [SerializeField] float maxSize = 10f;
    Camera cam;
    void Awake()
    {
        //Cursor.visible = false;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 viewportPos = cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                dragging = true;
                dragOrigin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            }
        }

        if (Input.GetMouseButton(1) && dragging)
        {
            Vector3 currentPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            Vector3 diff = dragOrigin - currentPos;

            transform.position += new Vector3(diff.x, diff.y, 0);
        }
        if (Input.GetMouseButtonUp(1)) dragging = false;
        float x = Mathf.Clamp(transform.position.x, -5.5f, 5.5f);
        float y = Mathf.Clamp(transform.position.y, 0f, 4f);    

        transform.position = new Vector3(x, y, transform.position.z);
        //scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newSize = cam.orthographicSize - (scrollInput * zoomSpeed);
            cam.orthographicSize = Mathf.Clamp(newSize, minSize, maxSize);
        }
    }
}