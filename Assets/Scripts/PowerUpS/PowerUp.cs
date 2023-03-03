using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(0f, 50f * Time.deltaTime, 0f, Space.Self);
    }
}
