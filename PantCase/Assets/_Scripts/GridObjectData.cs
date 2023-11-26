using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "GridObjectData", menuName = "BuildingData", order = 0)]
public class GridObjectData : ScriptableObject
{
    public Sprite sprite;
    public Vector2Int size;
    public ObjectType objectType;

    [ShowIf("objectType", ObjectType.ActiveBuilding)]
    public List<GridObjectData> productionData;
}

public enum ObjectType
{
    ActiveBuilding,
    PassiveBuilding,
    Unit,
}