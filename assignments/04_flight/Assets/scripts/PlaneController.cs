using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneController : MonoBehaviour
{
    public GameObject basketballPrefab;

    int fuel = 0;

    float forwardSpeed = 85f;

    float xRotationSpeed = 40f;
    float yRotationSpeed = 10f;
    float zRotationSpeed = 40f;

    Vector3 oldCamPos;

    public GameObject cameraObject;

    public GameObject particles;

    public GameObject myPlane;

    public TMP_Text fuelText;

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
        if (Input.GetKeyDown(KeyCode.Space) && fuel > 0)
        {
            forwardSpeed += 20;
            fuel--;
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
        cameraObject.transform.position = newCamPos;
        cameraObject.transform.LookAt(transform);
        oldCamPos = newCamPos;

        fuelText.text = fuel.ToString();
            
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            Debug.Log("ring get!");
            particles.SetActive(true);
            Vector3 pos = transform.position;
            Instantiate(particles, pos, Quaternion.identity, myPlane.transform);
            fuel = fuel + 1;
        }
    }


}