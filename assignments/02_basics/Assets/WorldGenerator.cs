using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
        public GameObject tapeParent;
        public GameObject rocketParent;
        public Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    GenerateTape();
    GenerateRockets();
  }

  void GenerateRockets()
  {
    for (int i = 0; i < 10; i++)
    {
      Vector3 pos = new Vector3(45, Random.Range(0, 60), Random.Range(-7, 70));
      GameObject RocketObj = Instantiate(rocketParent, pos, Quaternion.identity);
    }
  }
  void GenerateTape()
  {
    for (int i = 0; i < 35; i++)
    {
      Vector3 pos = new Vector3(0, 0, (i*2));
      GameObject tapeObj = Instantiate(tapeParent, pos, Quaternion.identity);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      GenerateRockets();
    }

  }
}
