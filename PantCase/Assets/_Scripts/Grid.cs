using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Grid
{
    public int width;
    public int height;
    public float cellSize;
    public Vector3 pivot;
    public Cell[,] grid;

    public Grid(int width, int height, float cellSize, Vector3 pivot)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.pivot = pivot;

        grid = new Cell[width, height];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = new Cell();
            }
        }
    }


    public bool IsInGrid(Vector3 worldPos)
    {
        Vector2Int gridPosition = GetGridPosition(worldPos);

        if (gridPosition.x < 0 || gridPosition.x >= width || gridPosition.y < 0 || gridPosition.y >= height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsInGrid(Vector2Int gridPos)
    {
        Vector2Int gridPosition = gridPos;

        if (gridPosition.x < 0 || gridPosition.x >= width || gridPosition.y < 0 || gridPosition.y >= height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * cellSize + new Vector3(cellSize, cellSize, 0) * 0.5f;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0) * cellSize + new Vector3(cellSize, cellSize, 0) * 0.5f;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x / cellSize), Mathf.FloorToInt(worldPosition.y / cellSize));
    }

    public bool CanPlaceOnGrid(Vector2Int gridPivot, Vector2Int size)
    {
        bool canPlace = true;


        for (int x = gridPivot.x; x < gridPivot.x + size.x; x++)
        {
            for (int y = gridPivot.y; y < gridPivot.y + size.y; y++)
            {
                if (x < 0 || x >= width || y < 0 || y >= height)
                {
                    canPlace = false;
                    break;
                }

                if (!grid[x, y].CanMove())
                {
                    canPlace = false;
                    break;
                }
            }
        }


        return canPlace;
    }

    public void PlaceOnGrid(Vector2Int gridPivot, Vector2Int size, GridObject cellObject)
    {
        for (int x = gridPivot.x; x < gridPivot.x + size.x; x++)
        {
            for (int y = gridPivot.y; y < gridPivot.y + size.y; y++)
            {
                grid[x, y].CellObject = cellObject;
                grid[x, y].CellState = CellState.Filled;
            }
        }
    }

    public void CleanOnGrid(Vector2Int gridPivot, Vector2Int size)
    {
        for (int x = gridPivot.x + size.x - 1; x >= gridPivot.x; x--)
        {
            for (int y = gridPivot.y + size.y - 1; y >= gridPivot.y; y--)
            {
                grid[x, y].CellObject = null;
                grid[x, y].CellState = CellState.Empty;
            }
        }
    }

    public void SetCell(Vector2Int gridPos, GridObject cellObject)
    {
        grid[gridPos.x, gridPos.y].CellObject = cellObject;
        grid[gridPos.x, gridPos.y].CellState = CellState.Filled;
    }

    public void DrawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y].CanMove() == false)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }


                Gizmos.DrawWireCube(GetWorldPosition(x, y), Vector3.one * (cellSize));
            }
        }
    }

    public Vector2Int FindClosestEmptyPoint(Vector2Int targetPoint)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(targetPoint);
        visited.Add(targetPoint);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (grid[current.x, current.y].CellState == CellState.Empty && grid[current.x, current.y].CanMove())
            {
                return current; // Found the closest empty point.
            }

            // Check neighbors
            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        // If no empty point is found, return an invalid position or handle it based on your needs.
        return new Vector2Int(-1, -1);
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int position)
    {
        // Define possible neighbors (up, down, left, right).
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = position + dir;

            // Check if the neighbor is within the grid boundaries.
            if (IsInGrid(neighbor))
            {
                yield return neighbor;
            }
        }
    }
}

[Serializable]
public class Cell
{
    private CellState _cellState;
    private GridObject _cellObject;


    public Cell()
    {
        _cellState = CellState.Empty;
    }

    public CellState CellState
    {
        get => _cellState;
        set => _cellState = value;
    }

    public GridObject CellObject
    {
        get => _cellObject;
        set => _cellObject = value;
    }


    public bool CanMove()
    {
        if (_cellState == CellState.Blocked || _cellState == CellState.Filled)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

public enum CellState
{
    Empty,
    Filled,
    Blocked
}