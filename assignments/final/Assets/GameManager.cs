using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject dialoguePanel;

    public int nextUtterance = 0;

    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaunchDialogue(VillagerScript person)
    {        
        dialoguePanel.SetActive(true);
        nameText.text = person.PersonName;
        dialogueText.text = person.utterances[nextUtterance];
        if (nextUtterance + 1 >= person.utterances.Length)
        {
            nextUtterance = 0;
        }
        else
        {
            nextUtterance = nextUtterance + 1;
        }
       
    }
}