using UnityEngine;
using System.Collections.Generic;

public class GameOfLife : MonoBehaviour
{

    bool[,] grid;
    int gridSize = 4;
    string gridString = "";
    string currentGridString;
    string nextGridString;


    Vector2Int aliveCell;
    Vector2Int deadCell;

    void Start()
    {
        grid = new bool[gridSize, gridSize];
        GenerateGrid();
        UpdateGrid(grid);

    }

    void Update()
    {



    }



    void GenerateGrid()
    {
        gridString = "";

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {


                if (Random.value < 0.5)
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
        Debug.Log(currentGridString);



    }

    void UpdateGrid(bool[,] grid)
    {



        nextGridString = "";
        bool[,] nextGrid = new bool[gridSize, gridSize];

        KillCells(grid, nextGrid);
        BirthCells(grid, nextGrid);

        if (grid == nextGrid) return;
        else
            grid = nextGrid;
        //new grid generation

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


        Debug.Log(nextGridString);

        //end of new generation test



    }


    void KillCells(bool[,] grid, bool[,] nextGrid)
    {
        List<Vector2Int> aliveCells = new List<Vector2Int>();



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
                Debug.Log($"Cell at  [{aliveCell.x},{aliveCell.y}] has been killed.");
            }
            else if (count == 2 || count == 3)
            {
                continue;
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
                Debug.Log($"Cell at  [{deadCell.x},{deadCell.y}] has been birthed.");
            }




        }



    }
}