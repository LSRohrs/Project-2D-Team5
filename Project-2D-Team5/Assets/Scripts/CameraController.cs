using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;

    private void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}
