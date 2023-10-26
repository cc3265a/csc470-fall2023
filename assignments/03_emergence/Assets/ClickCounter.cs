using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ClickCounter : MonoBehaviour
{
    public TMP_Text CounterText;
    public TMP_Text SpaceText;
    public TMP_Text StartText;

    public int counter = 5;
    public int spaced = -1;

    // Start is called before the first frame update
    void Start()
    {
        CounterText.SetText("5 Clicks Remaining");
        SpaceText.SetText("  Generations");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (counter < 1)
            {
                counter = 0;
            }
            else
            {
                counter--;
            }
            CounterText.SetText(counter.ToString() + " Clicks Remaining");
            SpaceText.SetText(spaced.ToString() + " Generations");
            StartText.SetText("");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter = 5;
            spaced++;
            SpaceText.SetText(spaced.ToString() + " Generations");
            CounterText.SetText(counter.ToString() + " Clicks Remaining");
            StartText.SetText("");
        }
    }
}
