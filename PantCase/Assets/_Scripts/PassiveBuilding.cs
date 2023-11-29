using UnityEngine;

public class PassiveBuilding : GridObject, IDamagable
{
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            view.Destroy();
            GridManager.Instance.grid.CleanOnGrid(gridPivot, gridObjectData.size);
        }
    }
}