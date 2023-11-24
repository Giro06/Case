using System;
using Giroo.Utility;
using UnityEngine;

namespace _Scripts
{
    public class GridFillTest : MonoBehaviour
    {
        public void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Vector2Int gridPos = GridManager.Instance.grid.GetGridPosition(mousePos);

                Debug.Log("Mouse pos: " + mousePos + "//  Grid Pos: " + gridPos);
                if (GridManager.Instance.grid.IsInGrid(mousePos))
                {
                    GridManager.Instance.grid.grid[gridPos.x, gridPos.y].CellState = CellState.Filled;
                }
            }
        }
    }
}