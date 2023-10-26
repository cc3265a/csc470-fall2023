using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    float bonusSpeed = 0;
    float forwardSpeed = 15;

    Vector3


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        float xRotation = vAxis * xRotation * Time.deltaTime;
        float yRotation = hAxis * yRotation * Time.deltaTime;
        float zRotation = hAxis * zRotation * Time.deltaTime;

        transform.Rotate(xRotation, yRotation, -zRotation, Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bonusSpeed = 20;
        }
        bonusSpeed -= 5 * Time.deltaTime;
        bonusSpeed = Mathf.Max(0, bonusSpeed);

        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * (forwardSpeed + bonusSpeed);
    }
}
