using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class GameOfLife : MonoBehaviour
{

    bool[,] grid;
    int gridSize = 10;
    string gridString = "";
    string currentGridString;
    string nextGridString;

    float timeSinceLastUpdate = 0;
    float updateInterval = 0.5f;

    Vector2Int aliveCell;
    Vector2Int deadCell;

    void Start()
    {
        grid = new bool[gridSize, gridSize];
        GenerateGrid(grid);

    }

    void Update()
    {

        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            UpdateGrid();
            timeSinceLastUpdate = 0f;
        }
    }



    void GenerateGrid(bool[,] grid)
    {
        gridString = "";

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {


                if (Random.value < 0.1)
                {
                    grid[i, j] = true;
                    gridString += "x  ";
                }

                else
                {
                    grid[i, j] = false;
                    gridString += ".  ";

                }


            }

            gridString += "\n";
        }

        currentGridString = gridString;
        UnityEngine.Debug.Log(currentGridString);



    }

    void UpdateGrid()
    {



        nextGridString = "";
        bool[,] nextGrid = new bool[gridSize, gridSize];

        KillCells(grid, nextGrid);
        BirthCells(grid, nextGrid);

        //new grid generation

        bool isFrameNew = false;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid[i, j] != nextGrid[i, j])
                {
                    isFrameNew = true;
                    break;
                }


            }
        }


        if (isFrameNew == false)
            return;

        else if (isFrameNew)
        {

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {

                    grid[i, j] = nextGrid[i, j];

                }

            }

        }

        PrintNewGrid(nextGrid);

        //end of new generation test



    }


    void KillCells(bool[,] grid, bool[,] nextGrid)
    {
        List<Vector2Int> aliveCells = new List<Vector2Int>();

        if (grid == null) return;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (grid[x, y])
                {
                    aliveCell = new Vector2Int(x, y);
                    aliveCells.Add(aliveCell);

                }
            }

        }


        foreach (Vector2Int aliveCell in aliveCells)
        {

            List<Vector2Int> neighbors = new List<Vector2Int>();

            for (int i = aliveCell.x - 1; i <= aliveCell.x + 1; i++)
            {
                for (int j = aliveCell.y - 1; j <= aliveCell.y + 1; j++)
                {
                    if (i == aliveCell.x && j == aliveCell.y)
                        continue;

                    if (i < 0 || i >= gridSize || j < 0 || j >= gridSize)
                        continue;

                    if (grid[i, j])
                    {
                        var neighbor = new Vector2Int(i, j);
                        neighbors.Add(neighbor);

                    }

                }

            }

            int count = neighbors.Count;

            if (count < 2 || count > 3)
            {
                nextGrid[aliveCell.x, aliveCell.y] = false;
                UnityEngine.Debug.Log($"Cell at  [{aliveCell.x},{aliveCell.y}] has been killed.");
            }
            else if (count == 2 || count == 3)
            {
                nextGrid[aliveCell.x, aliveCell.y] = true;
            }


        }
    }

    void BirthCells(bool[,] grid, bool[,] nextGrid)
    {
        List<Vector2Int> deadCells = new List<Vector2Int>();


        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (!grid[x, y])
                {
                    deadCell = new Vector2Int(x, y);
                    deadCells.Add(deadCell);

                }
            }

        }


        foreach (Vector2Int deadCell in deadCells)
        {

            List<Vector2Int> neighbors = new List<Vector2Int>();

            for (int i = deadCell.x - 1; i <= deadCell.x + 1; i++)
            {
                for (int j = deadCell.y - 1; j <= deadCell.y + 1; j++)
                {
                    if (i == deadCell.x && j == deadCell.y)
                        continue;

                    if (i < 0 || i >= gridSize || j < 0 || j >= gridSize)
                        continue;

                    if (grid[i, j])
                    {
                        var neighbor = new Vector2Int(i, j);
                        neighbors.Add(neighbor);

                    }

                }

            }

            int count = neighbors.Count;

            if (count == 3)
            {
                nextGrid[deadCell.x, deadCell.y] = true;
                UnityEngine.Debug.Log($"Cell at  [{deadCell.x},{deadCell.y}] has been birthed.");
            }




        }


    }

    void PrintNewGrid(bool[,] grid)

    {

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {


                if (grid[i, j])
                {
                    nextGridString += "x  ";
                }

                else
                {
                    nextGridString += ".  ";

                }


            }

            nextGridString += "\n";
        }

        UnityEngine.Debug.Log(nextGridString);
    }

}