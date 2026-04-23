using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;

    void Update()
    {
        if (player == null)
        {
            if (Camera.main != null)
            {
                player = Camera.main.transform;
            }
            else
            {
                return;
            }
        }

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        Vector3 directionToAngel = transform.position - player.position;
        float dot = Vector3.Dot(player.forward, directionToAngel.normalized);

        bool isBeingLookedAt = dot > 0.7f;

        if (!isBeingLookedAt)
        {
            if (directionToPlayer.magnitude > 2f)
            {
                transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
            }
        }

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            speed = 0f;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
        }
    }
}