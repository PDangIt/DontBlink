using UnityEngine;

public class PlayerBodyFollow : MonoBehaviour
{
    public Transform head; // VR Main Camera

    void Update()
    {
        Vector3 headPosition = head.position;

        // Follow head horizontally, but keep original height
        transform.position = new Vector3(headPosition.x, transform.position.y, headPosition.z);
    }
}