using System;
using Giroo.Utility;
using UnityEngine;


public class EventManager : Singleton<EventManager>
{
    public event Action<GridObjectData> OnCreateGameView;

    public event Action<IProducer, GridObjectData> OnSetSelected;
    public event Action OnUnSetSelected;

    public event Action<GridObjectData> OnSelectedSpawn;

    public void CreateGameView(GridObjectData gridObjectData)
    {
        OnCreateGameView?.Invoke(gridObjectData);
    }

    public void ProducerSelected(IProducer producer, GridObjectData gridObjectData)
    {
        OnSetSelected?.Invoke(producer, gridObjectData);
    }

    public void UnSelected()
    {
        OnUnSetSelected?.Invoke();
    }

    public void SpawnOnSelected(GridObjectData gridObjectData)
    {
        OnSelectedSpawn?.Invoke(gridObjectData);
    }
}