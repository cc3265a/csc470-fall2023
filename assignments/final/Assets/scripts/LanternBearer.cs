using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternBearer : MonoBehaviour
{
    int spokenToBearer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool giveLantern(VillagerScript vscript)
    {
        Debug.Log("lantern get");
        spokenToBearer += 1;
        int numWords = vscript.utterances.GetLength(0);
        Debug.Log("numwords = " + numWords);
        if (spokenToBearer >= numWords)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
