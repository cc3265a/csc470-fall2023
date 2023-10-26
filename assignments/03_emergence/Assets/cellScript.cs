using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public bool alive = false;
    public bool nextAlive = false;
    GameOfLifeGenerator gol;
    ClickCounter CC;

    public Color aliveColor;
    public Color deadColor;

    Renderer rend;

    public int x = -1;
    public int y = -1;


    // Start is called before the first frame update
    void Start()
    {
        GameObject golObj = GameObject.Find("GameOfLifeGeneratorObj");
        gol = golObj.GetComponent<GameOfLifeGenerator>(); 
        rend = gameObject.GetComponentInChildren<Renderer>();

        GameObject canvas = GameObject.Find("Canvas");
        CC = canvas.GetComponentInChildren<ClickCounter>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (CC.counter > 0)
        {
            alive = !alive;
            if (alive)
            {
                Debug.Log(x + ", " + y + " alive " + CountLiveNeighbors() + " neighbors");
            }
            else
            {
                Debug.Log(x + ", " + y + " dead " + CountLiveNeighbors() + " neighbors");
            }
            UpdateColor();
        }
    }

    public void UpdateColor()
    {
        Debug.Log("called update color");
        if (alive)
        {
            rend.material.color = aliveColor;
        }
        else
        {
            rend.material.color = deadColor;
        }
            
    }


    public int CountLiveNeighbors()
    {
        int alive = 0;
        if (x > 0 && x < 19 && y > 0 && y < 19) //not an edge
        {
            for (int xIndex = x - 1; xIndex <= x + 1; xIndex++)
            {
                for (int yIndex = y - 1; yIndex <= y + 1; yIndex++) //traverse array
                {
                    if (yIndex != y || xIndex != x) //dont count yourself
                    {
                        if (gol.cells[xIndex, yIndex].alive) //if alive
                        {
                            alive++; //increase count of alive neighbors
                        }
                    }
                }

            }
        }
        return alive;
    }

}
