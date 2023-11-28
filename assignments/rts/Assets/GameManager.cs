using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public event Action<UnitScript> UnitSelectedHappened;

    public GameObject cubePrefab;
    public GameObject cube2Prefab;

    UnitScript selectedUnit;
    public static GameManager SharedInstance; //refrence to self!

    public List<UnitScript> units = new List<UnitScript>();
    // Start is called before the first frame update

    public float rocks = 0;

    public TMP_Text rockText;

    void Awake()
    {
        if (SharedInstance != null)
        {
            Debug.Log("why do you have extra game manager??");
        }
        SharedInstance = this;
        Debug.Log("singleton created");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
         if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999999))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ground"))
                {
                    float locx = (int)hit.point.x;
                    float locy = (int)hit.point.y;
                    float locz = (int)hit.point.z;
                    //Vector3 locVect = 
                    //Instantiate(cubePrefab, hit.point, Quaternion.identity);
                    if (selectedUnit != null)
                    {
                        Debug.Log("we are here");
                        selectedUnit.setTarget(hit.point);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999999))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ground"))
                {
                   // Debug.Log(hit.point);
                    //Instantiate(cube2Prefab, hit.point, Quaternion.identity);
                    if (selectedUnit != null)
                    {
                        selectedUnit.setTarget(hit.point);
                    }
                }
            }
        }

    

    }
    public void SelectUnit(UnitScript unit)
    {
        //deselect any units that think they are selected
        foreach (UnitScript u in units)
        {
            u.selected = false;
            u.setUnitColor();
        }
        selectedUnit = unit;
        //Debug.Log(unit.name + " is selected");

        UnitSelectedHappened?.Invoke(unit);
    }

    public void IncreaseNumberOfRocks(float Newrocks)
    {
        rocks = rocks + Newrocks;
        //Debug.Log("Newrocks = " + Newrocks + " total rocks = " + rocks);
        rockText.text = rocks.ToString();
    }

}
