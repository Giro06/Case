using System;
using Giroo.Utility;
using UnityEngine;


public class GridManager : Singleton<GridManager>
{
    public Grid grid;

    public int width;
    public int height;
    public float cellSize;
    public Vector3 pivot;
    public GameObject gridObject;

    public void Start()
    {
        InitializeGrid(width, height, cellSize, pivot);
        CreaateGridView();
    }
    
    public void InitializeGrid(int width, int height, float cellSize, Vector3 pivot)
    {
        grid = new Grid(width, height, cellSize, pivot);
    }

    public void CreaateGridView()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject gameObject = Instantiate(gridObject, grid.GetWorldPosition(x, y), Quaternion.identity);
                gameObject.transform.SetParent(transform);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (grid != null)
        {
            grid.DrawGrid();
        }
    }
}