using System;
using Giroo.Utility;
using UnityEngine;


public class InputManager : Singleton<MonoBehaviour>
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);

            if (GridManager.Instance.grid.IsInGrid(mousePos))
            {
                Vector2Int gridPoint = GridManager.Instance.grid.GetGridPosition(mousePos);

                if (GridManager.Instance.grid.grid[gridPoint.x, gridPoint.y].CellState != CellState.Empty)
                {
                    Debug.Log("Clicked on cell with state: " + GridManager.Instance.grid.grid[gridPoint.x, gridPoint.y].CellObject.gridObjectData.name);
                }
                else
                {
                    Debug.Log("Clicked on empty cell");
                }
            }
        }
    }
}