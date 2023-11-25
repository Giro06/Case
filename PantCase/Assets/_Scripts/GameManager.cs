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
        EventManager.Instance.OnCreateGameView -= CreateGridObjectView;
    }

    public void CreateGridObjectView(GridObjectData gridObjectData)
    {
        this.gridObjectView = this.gridObjectView;
        GridObjectView gridObjectView = Instantiate(this.gridObjectView, Vector3.zero, Quaternion.identity);
        gridObjectView.Initialize(gridObjectData);
    }
}