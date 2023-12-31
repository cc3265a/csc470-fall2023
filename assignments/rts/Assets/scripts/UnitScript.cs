using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public float health = 100;
    float elapsed = 0f;

    public Renderer bodyRenderer;
    public string name;
    public Color selectedColor;
    public Color hoverColor;
    public Color deadColor;
    Color defaultColor;

    bool hover = false;

    public bool selected = false;

    Vector3 target;

    public CharacterController cc;
    public float moveSpeed = 10;
    public bool hasTarget = false;
    public Animator animator;

    public GameManager gm;

    public GameObject HurtParticles;
    public GameObject HealingParticles;

    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        defaultColor = bodyRenderer.material.color;
        Debug.Log("unit created");
        GameManager.SharedInstance.units.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.SharedInstance.units.Remove(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            Vector3 vectorToTarget = (target - transform.position).normalized;

            float step = 5 * Time.deltaTime;
            Vector3 rotatedTowardsVector = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
            rotatedTowardsVector.y = 0;
            transform.forward = rotatedTowardsVector;

            Vector3 amountToMove = transform.forward * moveSpeed * Time.deltaTime;
            cc.Move(vectorToTarget * moveSpeed * Time.deltaTime);

            Debug.Log(name + " is walking!");
            animator.SetBool("isWalking",true);
            animator.SetBool("isMining", false);
            animator.SetBool("isHealing", false);

            if (Vector3.Distance(transform.position, target) < 0.5f)
            {
                hasTarget = false;
                Debug.Log(name + " is not walking!");
                animator.SetBool("isWalking", false);
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Rocks"))
        {
            //Debug.Log("rocks");
            if (health <= 0) //dead
            {
                isAlive = false;
                animator.SetBool("dead", true);
                health = 0;
                bodyRenderer.material.color = deadColor;
                //idle
                animator.SetBool("isMining", false);
            }
            else //alive
            {
                animator.SetBool("isMining", true);
                //Debug.Log("else");
                health = health - (1 * Time.deltaTime);
                
                elapsed += Time.deltaTime;
                if (elapsed >= 1f)
                {
                    //Debug.Log(elapsed + "elasped");
                    elapsed = elapsed % 1f;
                    gm.IncreaseNumberOfRocks(1);
                }

                if (health < 20)
                {
                    Destroy(Instantiate(HurtParticles, transform.position + Vector3.up * 3, Quaternion.identity), .5f);
                }

            }

        }
        if (other.CompareTag("Tent"))
        {
            Debug.Log(name + " is being healed!");
            if (health >= 100)
            {
                animator.SetBool("isHealing", false);
                health = 100;
                //idle
            }
            else //heal
            {
                animator.SetBool("isHealing", true);
                health = health + (1 * Time.deltaTime);
                Destroy(Instantiate(HealingParticles, transform.position + Vector3.up * 3, Quaternion.identity), .5f);
            }
        }
    }

    public void setTarget(Vector3 t)
    {
        if (isAlive)
        {
            target = t;
            hasTarget = true;
        }
    }
    private void OnMouseDown()
    {
        GameManager.SharedInstance.SelectUnit(this);
        selected = true;
        setUnitColor();
        Debug.Log("yippee!");
    }
    private void OnMouseEnter()
    {
        hover = true;
        setUnitColor();
    }
    private void OnMouseExit()
    {
        hover = false;
        setUnitColor();
    }
    public void setUnitColor()
    {
        if (selected)
        {
            bodyRenderer.material.color = selectedColor;
        }
        else if (hover)
        {
            bodyRenderer.material.color = hoverColor;
        }
        else
        {
            bodyRenderer.material.color = defaultColor;
        }
    }
}
