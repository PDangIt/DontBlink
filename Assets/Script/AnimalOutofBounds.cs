using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalOutofBounds : MonoBehaviour
{
    private float bottomBound = -3;
    private float bottomDeath = -5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < bottomBound)
        {
            Debug.Log("Game Over!");
        }
        
        if(transform.position.z < bottomDeath)
        {
            Destroy(gameObject);
        }
        
    }
}
