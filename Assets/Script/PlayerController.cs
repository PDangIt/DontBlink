using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public float speed = 25.0f;
    public float sprintMultiplier = 2.0f;
    public float mouseSensitivity = 200f;

    public GameObject projectile;

    public Transform cameraTransform; // PC aim
    public Transform vrFirePoint;     // VR controller aim

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.Z))
        {
            currentSpeed *= sprintMultiplier;
        }

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        transform.position += moveDirection * currentSpeed * Time.deltaTime;

        if (!XRSettings.isDeviceActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }

        if (!XRSettings.isDeviceActive && Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = cameraTransform.position + cameraTransform.forward * 2f;
            Quaternion spawnRot = Quaternion.LookRotation(cameraTransform.forward);

            Instantiate(projectile, spawnPos, spawnRot);
        }

    }
}