using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "GridObjectData", menuName = "GridObjectData", order = 0)]
public class GridObjectData : ScriptableObject
{
    public Sprite sprite;
    public Vector2Int size;
    public ObjectType objectType;
    public Stats stats;

    [ShowIf("objectType", ObjectType.ActiveBuilding)]
    public ProductionData productionData;
}

public enum ObjectType
{
    ActiveBuilding,
    PassiveBuilding,
    Unit,
}