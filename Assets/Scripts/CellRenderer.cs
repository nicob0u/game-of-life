using UnityEngine;

public class CellRenderer : MonoBehaviour
{
    public GameObject deadCellPrefab;
    public GameObject aliveCellPrefab;
    public GameOfLife game;
    GameObject[,] visualGrid;
    GameObject gridCell;


    void OnEnable()
    {
        game.onGridInitialized += SpawnCells;
        game.onGridUpdated += UpdateGridVisuals;
    }
    void OnDisable()
    {
        game.onGridInitialized -= SpawnCells;
        game.onGridUpdated -= UpdateGridVisuals;

    }

    void SpawnCells()
    {
        visualGrid = new GameObject[game.gridSize, game.gridSize];

        for (int i = 0; i < game.gridSize; i++)
        {
            for (int j = 0; j < game.gridSize; j++)
            {
                Vector2 pos = new Vector2(i, j);
                GameObject prefab = game.grid[i, j] ? aliveCellPrefab : deadCellPrefab;
                visualGrid[i, j] = Instantiate(prefab, pos, Quaternion.identity);

            }
        }


    }

    void UpdateGridVisuals()
    {
        for (int i = 0; i < game.gridSize; i++)
        {
            for (int j = 0; j < game.gridSize; j++)
            {
                bool alive = game.grid[i, j];
                GameObject current = visualGrid[i, j];
                if (alive && current.CompareTag("DeadCell"))
                {
                    Destroy(current);
                    visualGrid[i, j] = Instantiate(aliveCellPrefab, new Vector2(i, j), Quaternion.identity);
                }
                if (!alive && current.CompareTag("AliveCell"))
                {
                    Destroy(current);
                    visualGrid[i, j] = Instantiate(deadCellPrefab, new Vector2(i, j), Quaternion.identity);
                }

            }
        }

    }
}
