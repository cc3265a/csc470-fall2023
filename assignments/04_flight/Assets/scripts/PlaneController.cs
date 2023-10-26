using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameObject basketballPrefab;

    //int score = 0;

    float forwardSpeed = 85f;

    float xRotationSpeed = 40f;
    float yRotationSpeed = 10f;
    float zRotationSpeed = 40f;

    Vector3 oldCamPos;

    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // GET INPUT
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // LAUNCH BASKETBALL
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 inFrontOfPlane = transform.position + transform.forward * 5;
            GameObject ball = Instantiate(basketballPrefab, inFrontOfPlane, transform.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddForce(ball.transform.forward * 2500);
        }

        // ROTATE
        float xRotation = vAxis * xRotationSpeed * Time.deltaTime;
        float yRotation = hAxis * yRotationSpeed * Time.deltaTime;
        float zRotation = hAxis * zRotationSpeed * Time.deltaTime;

        transform.Rotate(xRotation, yRotation, -zRotation, Space.Self);

        // BOOST
        if (Input.GetKeyDown(KeyCode.Space))
        {
            forwardSpeed += 20;
        }

        // GRAVITY
        forwardSpeed -= transform.forward.y * 15 * Time.deltaTime;
        forwardSpeed = Mathf.Max(0, forwardSpeed);

        //terrain collision
        float terrainY = Terrain.activeTerrain.SampleHeight(transform.position);
        if (transform.position.y < terrainY)
        {
            transform.position = new Vector3(transform.position.x, terrainY, transform.position.z);
            forwardSpeed -= 100 * Time.deltaTime;
            Debug.Log("crash!");
        }

        // MOVE FORWARD
        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * forwardSpeed;




        // CAMERA
        Vector3 newCamPos = transform.position + -transform.forward * 10 + Vector3.up * 5;
        if (oldCamPos == null)
        {
            oldCamPos = newCamPos;
        }
        cameraObject.transform.position = (newCamPos + oldCamPos) / 2f;
        cameraObject.transform.LookAt(transform);
        oldCamPos = newCamPos;


            
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            Debug.Log("ring get!");
        }
    }


}