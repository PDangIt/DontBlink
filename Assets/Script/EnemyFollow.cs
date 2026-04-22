using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    public Transform player;

    void Start()
    {
        GameObject cam = GameObject.Find("VR Player Camera");

        if (cam != null)
        {
            player = cam.transform;
        }
        else if (Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
{
    if (player == null) return;

    Vector3 directionToPlayer = player.position - transform.position;
    directionToPlayer.y = 0f;

    // Check if player is looking at the angel
    Vector3 directionToAngel = transform.position - player.position;
    float dot = Vector3.Dot(player.forward, directionToAngel.normalized);

    bool isBeingLookedAt = dot > 0.7f;

    if (!isBeingLookedAt)
    {
        // MOVE
        if (directionToPlayer.magnitude > 2f)
        {
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }
    }

    // ALWAYS face player
    if (directionToPlayer != Vector3.zero)
    {
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }
}
}