using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
    public List<GridObjectData> GetProductionData();
    public Vector2Int GetSpawnPoint();
    public void SetSpawnPoint(Vector2Int spawnPoint);
    
    public void Spawn(GridObjectData gridObjectData);
}