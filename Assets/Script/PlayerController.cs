using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 25.0f;
    public float sprintMultiplier = 2.0f;
    public float mouseSensitivity = 200f;
    public GameObject projectile;
    public Transform firePoint;

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        // WASD Movement
        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;

        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;

        if (Input.GetKey(KeyCode.A))
            moveX = -1f;

        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // Sprint
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.Z))
        {
            currentSpeed *= sprintMultiplier;
        }

        // Movement relative to player direction
        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        transform.position += moveDirection * currentSpeed * Time.deltaTime;

        // Mouse rotation (left/right)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Shoot projectile
        if (Input.GetMouseButtonDown(0)) 
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
    }
}