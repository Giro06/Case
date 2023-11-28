using System.Collections.Generic;
using UnityEngine;

public class ActiveBuilding : GridObject, IProducer, IDamagable
{
    public List<GridObjectData> productionData;
    public Vector2Int spawnPoint;


    public List<GridObjectData> GetProductionData()
    {
        return productionData;
    }

    public Vector2Int GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void SetSpawnPoint(Vector2Int spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public void Spawn(GridObjectData gridObjectData)
    {
        Vector2Int getClosestEmptyPoint = GridManager.Instance.grid.FindClosestEmptyPoint(GetSpawnPoint());
        GameManager.Instance.CreateSoldier(gridObjectData, getClosestEmptyPoint);
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GridManager.Instance.grid.CleanOnGrid(gridPivot, gridObjectData.size);
            Object.Destroy(view);
        }
    }
}