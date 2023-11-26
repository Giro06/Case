using System;
using Giroo.Utility;
using UnityEngine;


public class EventManager : Singleton<EventManager>
{
    public event Action<GridObjectData> OnCreateGameView;

    public void CreateGameView(GridObjectData gridObjectData)
    {
        OnCreateGameView?.Invoke(gridObjectData);
    }
}