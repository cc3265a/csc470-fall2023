using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager SharedInstance;
    public TMP_Text nameText;
    public TMP_Text keyText;


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
    public void LaunchDialogue(string doorText)
    {
        nameText.text = doorText;
        
    }
    public void KeyGotText(string txt)
    {
        keyText.text = txt;
    }
}

