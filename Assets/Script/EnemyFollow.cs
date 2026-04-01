using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 0.001f;
    public Transform player;

    void Start()
    {
        player = GameObject.Find("VR Player Camera").transform; // VR headset position
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;

        // 🔑 Ignore vertical movement
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > 2f) // stopping distance
        {
            direction = direction.normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        // 🔑 Rotate only on Y axis (no tilting)
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Simple stop or redirect
            speed = 0f;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
        }
    }
}