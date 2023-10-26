using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public TMP_Text nameText;
    public TMP_Text DialogueText;

    public GameObject DialoguePanel;


    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("playerTime");
        }
    }
    public void LaunchDialogue(VillagerScript chicken)
    {
        nameText.text = chicken.name;
        DialogueText.text = chicken.monologue[0];
        DialogueText.text = chicken.monologue[0];
    }
}

