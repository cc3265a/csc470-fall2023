using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject dialoguePanel;
    public GameObject endPanel;

    public int nextUtterance = 0;

    public PostProcessVolume volume;

    public Grain grain;

    public GameObject blackOutSquare;

    public GameObject player;

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
        Debug.Log(dialoguePanel);
        nameText.text = person.PersonName;
        dialogueText.text = person.utterances[nextUtterance];
        Debug.Log(person.utterances[0]);
        if (nextUtterance + 1 >= person.utterances.Length)
        {
            nextUtterance = 0;
        }
        else
        {
            nextUtterance = nextUtterance + 1;
        }
       
    }

    public void fadeToBlack(float fadeSpeed)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        blackOutSquare.GetComponent<Image>().color = objectColor;

        if (objectColor.a >= 1)
        {
            player.GetComponent<PlayerController>().TunnelTransport();
            Debug.Log("called tunnel transport");
        }

    }
    public void unFadeToBlack(float fadeSpeed)
    {
        Debug.Log("unfade");
        Color objectColor = blackOutSquare.GetComponent<Image>().color;

        while (objectColor.a > 0)
        {
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, -fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor;
        }
    }
}