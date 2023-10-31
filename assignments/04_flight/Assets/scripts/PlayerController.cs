using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;
    float forwardSpeed = 10;
    float rotateSpeed = 60;
    float jumpForce = 18;
    float gravityModifier = 5f;

    float yVelocity = 0;

    Vector3 oldCamPos;
    public GameObject cameraObject;

    bool hasKey = false;

    // Start is called before the first frame update

    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);


        if (!cc.isGrounded)
        {
            yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        else
        {
            yVelocity = -1;
            if (Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }
        }

        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;
        //Debug.Log(amountToMove.magnitude);
        cc.Move(amountToMove * Time.deltaTime);
        //Debug.Log(yVelocity);


        Vector3 newCamPos = transform.position + -transform.forward * 10 + Vector3.up * 5;
        if (oldCamPos == null)
        {
            oldCamPos = newCamPos;
        }
        cameraObject.transform.position = (newCamPos + oldCamPos) / 2f;
        cameraObject.transform.LookAt(transform);
        oldCamPos = newCamPos;

        GameObject door = GameObject.FindGameObjectWithTag("door");
        float d = Vector3.Distance(transform.position, door.transform.position);
        Debug.Log(d);
        if (d <= 4)
        {
            Debug.Log("door near!");
            if (hasKey != true)
            {
                GameManager.SharedInstance.LaunchDialogue("You need a key to open this door");
            }
            else
            {
                GameManager.SharedInstance.LaunchDialogue("Press E to use key");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(door);
                    GameManager.SharedInstance.LaunchDialogue("");
                    GameManager.SharedInstance.KeyGotText("");
                }
            }
        }
        else
        {
            GameManager.SharedInstance.LaunchDialogue("");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyItem"))
        {
            Debug.Log("key get!");
            hasKey = true;
            GameManager.SharedInstance.KeyGotText("Key Got");
        }
    }
}
