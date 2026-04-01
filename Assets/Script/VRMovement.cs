using UnityEngine;
using UnityEngine.InputSystem;

public class VRMovement : MonoBehaviour
{
    public InputActionProperty moveInput; // left joystick
    public float speed = 60f;
    public Transform head; // VR Main Camera
    public InputActionProperty turnInput; // right joystick
    public float turnSpeed = 240f;

    void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        // Get forward/right based on head direction (flat only)
        Vector3 forward = head.forward;
        Vector3 right = head.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * input.y + right * input.x;

        transform.position += move * speed * Time.deltaTime;


        Vector2 turn = turnInput.action.ReadValue<Vector2>();

        float turnAmount = turn.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnAmount, 0);
    }
}