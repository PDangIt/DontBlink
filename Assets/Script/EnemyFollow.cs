using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    public float stopDistance = 2f;
    public float killDistance = 4f;
    public float lookThreshold = 0.7f;

    public Transform player;
    private GameManager gm;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else if (Camera.main != null)
            player = Camera.main.transform;

        gm = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        float distance = directionToPlayer.magnitude;

        // Death trigger by distance
        if (distance <= killDistance)
        {
            if (gm != null)
                gm.PlayerDied();

            return;
        }

        // Check if player is looking at angel
        Vector3 directionToAngel = transform.position - player.position;
        directionToAngel.y = 0f;

        float dot = Vector3.Dot(player.forward, directionToAngel.normalized);
        bool isBeingLookedAt = dot > lookThreshold;

        // Move only when NOT being looked at
        if (!isBeingLookedAt && distance > stopDistance)
        {
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }

        // Always face player
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }
}