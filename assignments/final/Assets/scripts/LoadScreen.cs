using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions and Comments");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("ifIScreamWillAnyoneHear");
    }

}
