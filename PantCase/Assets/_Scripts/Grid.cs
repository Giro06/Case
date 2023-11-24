using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

[Serializable]
public class Cell
{
    private CellState _cellState;

    public CellState CellState
    {
        get => _cellState;
        set => _cellState = value;
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