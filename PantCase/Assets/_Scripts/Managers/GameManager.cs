using System;
using Giroo.Utility;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GridObjectView gridObjectView;


    public void Start()
    {
        EventManager.Instance.OnCreateGameView += CreateGridObjectView;
    }

    public void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnCreateGameView -= CreateGridObjectView;
        }
    }

    public void CreateGridObjectView(GridObjectData gridObjectData)
    {
        this.gridObjectView = this.gridObjectView;
        GridObjectView gridObjectView = Instantiate(this.gridObjectView, Vector3.zero, Quaternion.identity);
        gridObjectView.Initialize(gridObjectData);
    }

    public void CreateSoldier(GridObjectData gridObjectData, Vector2Int pos)
    {
        Vector2Int gridPoint = pos - gridObjectData.size / 2;
        Vector3 gridWorldPos = GridManager.Instance.grid.GetWorldPosition(gridPoint + gridObjectData.size / 2);

        GridObjectView gridObjectView = Instantiate(this.gridObjectView, gridWorldPos, Quaternion.identity);
        gridObjectView.Initialize(gridObjectData);


        transform.position = gridWorldPos;

        gridObjectView.Place(gridPoint);
    }
}