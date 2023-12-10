using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // These are the variables that will scale the effect of the movement, gravity,
    // and rotation code in update.
    float forwardSpeed = 6;
    float rotateSpeed = 60;
    float jumpForce = 18;
    float gravityModifier = 4.5f;

    // This is the variable we will use to accumulate gravity.
    float yVelocity = 0;

    CharacterController cc;

    public GameManager gm;

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

        // --- ROTATION ---
        // Rotate on the y axis based on the hAxis value
        // NOTE: If the player isn't pressing left or right, hAxis will be 0 and there will be no rotation
        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

        // --- DEALING WITH GRAVITY ---
        if (!cc.isGrounded)
        {
            // If we go in this block of code, cc.isGrounded is false, which means
            // the last time cc.Move was called, we did not try to enter the ground.
            yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        else
        {
            // If we are in this block of code, we are on the ground.
            // Set the yVelocity to be some small number to try to push us into
            // the ground and thus make cc.isGrounded be true.
            yVelocity = -1;


            // JUMP. When the player presses space, set yVelocity to the jump force. This will immediately
            // make the player start moving upwards, and gravity will start slowing the movement upward
            // and eventually make the player hit the ground (thus landing in the 'if' statment above)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }



            float castDistance = 5;
            Vector3 positionToRayCastFrom = transform.position + Vector3.up * 1.8f;
            Ray ray = new Ray(positionToRayCastFrom, transform.forward);
            Debug.DrawRay(positionToRayCastFrom, transform.forward * castDistance, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, castDistance))
            {
                // If we get in here, we hit something
                // 'hit' contains information about what we hit

                //Destroy(hit.collider.gameObject);
                Debug.Log("Get gotted" + hit);
            }
            else
            {
                //Debug.Log("Didn't hit anything with raycast");
                gm.dialoguePanel.SetActive(false);
            }
        }

        // --- TRANSLATION ---
        // Move the player forward based on the vAxis value
        // Note, If the player isn't pressing up or down, vAxis will be 0 and there will be no movement
        // based on input. However, yVelocity will still move the player downward.
        Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
        amountToMove.y = yVelocity;

        // This will move the player according to the forward vector and the yVelocity using the
        // CharacterController.
        cc.Move(amountToMove * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckPersonTalking();
        }

    }

    void CheckPersonTalking()
    {
        GameObject[] people = GameObject.FindGameObjectsWithTag("Villager");
        float closest = 999999999;
        GameObject closestPerson = null;
        for (int i = 0; i < people.Length; i++)
        {
            float d = Vector3.Distance(transform.position, people[i].transform.position);
            if (d < closest)
            {
                closest = d;
                closestPerson = people[i];
            }
        }


        if (closestPerson != null)
        {
            Vector3 vectorToPerson = (closestPerson.transform.position - transform.position).normalized;
            float angleToPerson = Vector3.Angle(transform.forward, vectorToPerson);
            if (angleToPerson < 45)
            {
                GameManager.SharedInstance.LaunchDialogue(closestPerson.GetComponent<VillagerScript>());
            }
            else
            {
                GameManager.SharedInstance.dialoguePanel.SetActive(false);
            }
        }
    }
}