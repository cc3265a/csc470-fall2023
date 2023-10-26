using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;
    float forwardSpeed = 6;
    float rotateSpeed = 60;
    float jumpForce = 18;
    float gravityModifier = 5f;

    float yVelocity = 0;

    Vector3 oldCamPos;
    public GameObject cameraObject;

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
        cc.Move(amountToMove * Time.deltaTime);
        Debug.Log(yVelocity);


        Vector3 newCamPos = transform.position + -transform.forward * 10 + Vector3.up * 5;
        if (oldCamPos == null)
        {
            oldCamPos = newCamPos;
        }
        cameraObject.transform.position = (newCamPos + oldCamPos) / 2f;
        cameraObject.transform.LookAt(transform);
        oldCamPos = newCamPos;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckChickenTalking();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyItem"))
        {
            Debug.Log("key get!");
        }
    }
    void CheckChickenTalking()
    {
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("villager");
        float closest = 999999999999;
        GameObject closestChicken = null;
        for (int i = 0; i < chickens.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, chickens[i].transform.position);
            if (distance < closest)
            {
                closest = distance;
                closestChicken = chickens[i];
            }
        }
        if (closestChicken != null)
        {
            Vector3 vectorToChicken = (closestChicken.transform.position - transform.position).normalized;
            float angleToChicken = Vector3.Angle(transform.forward, vectorToChicken);
            if (angleToChicken < 45)
            {
                GameManager.SharedInstance.LaunchDialogue(closestChicken.GetComponent<VillagerScript>());
            }
        }

    }
}
