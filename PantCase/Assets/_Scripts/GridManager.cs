using System;
using Giroo.Utility;
using UnityEngine;


public class GridManager : Singleton<GridManager>
{
    public Grid grid;

    public int width;
    public int height;
    public float cellSize;


    public void Start()
    {
        InitializeGrid(width, height, cellSize);
    }

    public void LateUpdate()
    {
    }

    public void InitializeGrid(int width, int height, float cellSize)
    {
        grid = new Grid(width, height, cellSize);
    }

    public void OnDrawGizmos()
    {
        if (grid != null)
        {
            grid.DrawGrid();
        }
    }
}