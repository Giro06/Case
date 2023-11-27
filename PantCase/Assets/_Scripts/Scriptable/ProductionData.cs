using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProductionData", menuName = "ProductionData", order = 0)]
public class ProductionData : ScriptableObject
{
    public List<GridObjectData> productionData;
}