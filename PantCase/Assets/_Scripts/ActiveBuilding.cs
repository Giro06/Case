using System.Collections.Generic;

public class ActiveBuilding : GridObject, IProducer
{
    public List<GridObjectData> productionData;

    public List<GridObjectData> GetProductionData()
    {
        return productionData;
    }
}