using UnityEngine;

public class Unit : GridObject, IMoveable
{
    public void Move(Vector2Int pos)
    {
        var data = GridManager.Instance.grid.grid[gridPivot.x, gridPivot.y];
        GridManager.Instance.grid.grid[gridPivot.x, gridPivot.y] = new Cell();
        data.CellObject.gridPivot = pos;
        GridManager.Instance.grid.grid[pos.x, pos.y] = data;

        Vector3 gridWorldPos = GridManager.Instance.grid.GetWorldPosition(pos + gridObjectData.size / 2);
        view.transform.position = gridWorldPos;
    }

    public Vector2Int GetCurrentPosition()
    {
        return gridPivot;
    }
}