using System.Collections.Generic;
using UnityEngine;

public class ActiveBuilding : GridObject, IProducer, IDamagable
{
    public List<GridObjectData> productionData;

    public List<GridObjectData> GetProductionData()
    {
        return productionData;
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