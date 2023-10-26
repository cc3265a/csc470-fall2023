using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    public string name;
    public string[] monologue;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, Random.Range(0, 360), 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
