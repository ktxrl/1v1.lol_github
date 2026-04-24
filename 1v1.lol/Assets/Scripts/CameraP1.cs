using UnityEngine;

public class CameraP1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 vec3;

    void Update()
    {
        //float x = Mathf.Clamp(player.transform.position.x, -5.5f, 5.5f);
        //float y = Mathf.Clamp(player.transform.position.y, 0f, 4f);
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), ref vec3, .15f);
    }
}
