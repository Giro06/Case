using UnityEngine;


[CreateAssetMenu(fileName = "GridObjectData", menuName = "BuildingData", order = 0)]
public class GridObjectData : ScriptableObject
{
    public Sprite sprite;
    public Vector2Int size;
    public ObjectType objectType;
    
    
}

public enum ObjectType
{
    Building,
    Unit,
}