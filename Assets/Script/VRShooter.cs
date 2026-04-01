using UnityEngine;
using UnityEngine.InputSystem;

public class VRShooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public InputActionProperty triggerAction;

    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
    }
}