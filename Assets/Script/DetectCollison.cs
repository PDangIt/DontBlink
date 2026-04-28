using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollison : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100f)
        {
            Destroy(gameObject);
        }
        
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // only destroy projectile
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
        }
    }
}
