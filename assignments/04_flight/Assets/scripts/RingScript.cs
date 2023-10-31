using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ring initiated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ring!!");
            Renderer rend = gameObject.GetComponentInChildren<Renderer>();
            //Destroy(gameObject);
            Color customColor = new Color(0.4f, 0.9f, 0.7f, 1.0f);
            rend.material.SetColor("_Color", customColor);
        }
    }
}
