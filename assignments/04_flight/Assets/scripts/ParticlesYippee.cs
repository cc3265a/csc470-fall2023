using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesYippee : MonoBehaviour
{
    Vector3 particlePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPartPos = transform.position + -transform.forward * 10 + Vector3.up * 5;
        if (particlePos == null)
        {
            particlePos = newPartPos;
        }
        particlePos = newPartPos;
    }
}
