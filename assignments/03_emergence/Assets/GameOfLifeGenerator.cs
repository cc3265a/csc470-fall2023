using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOfLifeGenerator : MonoBehaviour
{
    public GameObject cellPrefab;
    //public Rigidbody rb;
    private int id;
    public TMP_Text WinText;

    // Create a 2D array of CellScripts
    public cellScript[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the 2D array that we will use to store the state of the
        // Cells.
        cells = new cellScript[20, 20];

        // Using nested for loops is a good way to create patterns like (0,0),
        // (0,1), (0,2), (0,3)... (2,0), (2,1), (2,2).... (5,0), (5,1)... etc.
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                // Create a position based on x, y
                Vector3 pos = transform.position;
                float cellWidth = 1f;
                float spacing = 0.00f;
                pos.x = pos.x + x * (cellWidth + spacing);
                pos.z = pos.z + y * (cellWidth + spacing);
                GameObject cellObj = Instantiate(cellPrefab, pos, transform.rotation);
                // (x,y) is the index in the 2D array. Store a reference to the
                // CellScript of the instantiated object because that is the
                // object that contains the information we will be intereated in
                // (the 'alive' variable.
                cells[x, y] = cellObj.GetComponent<cellScript>();
                cells[x, y].x = x;
                cells[x, y].y = y;
                if (x > 0 && x < 19 && y > 0 && y < 19)
                {
                    cells[x, y].alive = (Random.value < 0.2f);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space pressed");
            CellUpdate();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    cells[x, y].alive = cells[x, y].nextAlive;
                    cells[x, y].UpdateColor();
                }
            }
        }
        int numDead = 0;
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (cells[x, y].alive)
                {
                    return;
                }
                else
                {
                    numDead++;
                }
            }
        }
        if (numDead == 400)
        {
            WinText.SetText("You Win!");
        }

        void CellUpdate() //every cell decides whether it should be alive or dead
        {
            //Debug.Log("cellUpdate called");
            //traverse 2d array of cells
            for (int x = 1; x < 19; x++)
            {
                for (int y = 1; y < 19; y++)
                {
                    //Debug.Log("a");
                    int aliveNeighbors = cells[x, y].CountLiveNeighbors();

                    //Debug.Log("we escaped the count live neighbors!");

                    //we've counted the number of alive neighbors for this cell; now:
                    if (aliveNeighbors < 2) //dying of loneliness
                    {
                        cells[x, y].nextAlive = false;
                    }
                    else if (aliveNeighbors < 4 && cells[x, y].alive) //stays alive
                    {
                        cells[x, y].nextAlive = true;
                    }
                    else if (aliveNeighbors == 3) //be born
                    {
                        cells[x, y].nextAlive = true;
                    }
                    else //dies
                    {
                        cells[x, y].nextAlive = false;
                    }
                }
            }
        }
    }
}