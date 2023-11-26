using UnityEngine;

public interface IMoveable
{
    public void Move(Vector2Int pos);
    public Vector2Int GetCurrentPosition();
}