using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    public float stopDistance = 2f;
    public float killDistance = 4f;
    public float lookThreshold = 0.75f;

    public Transform player;
    public Transform head;
    private GameManager gm;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        if (head == null && Camera.main != null)
            head = Camera.main.transform;

        gm = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (player == null || head == null) return;

        // --- DISTANCE CHECK ---
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        if (distance <= killDistance)
        {
            if (gm != null)
                gm.PlayerDied();

            return;
        }

        // --- LOOK CHECK ---
        Vector3 directionToAngel = (transform.position - head.position).normalized;

        float dot = Vector3.Dot(head.forward, directionToAngel);
        bool isBeingLookedAt = dot > lookThreshold;

        // --- MOVEMENT ---
        if (!isBeingLookedAt && distance > stopDistance)
        {
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }

        // --- FACE PLAYER ---
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }
}