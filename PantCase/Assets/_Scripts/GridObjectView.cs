using System;
using System.Text;
using Giroo.Utility;
using UnityEngine;

public class GridObjectView : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private GridObjectData _gridObjectData;
    private Vector2Int _placementPivot;
    public bool followCursor;

    public void Initialize(GridObjectData gridObjectData)
    {
        this._gridObjectData = gridObjectData;
        spriteRenderer.sprite = gridObjectData.sprite;
        followCursor = true;
        Vector3 pivot = Vector3.zero;
        if (_gridObjectData.size.x % 2 == 0)
        {
            pivot -= new Vector3(0.5f, 0, 0);
        }

        if (_gridObjectData.size.y % 2 == 0)
        {
            pivot -= new Vector3(0, 0.5f, 0);
        }

        spriteRenderer.transform.localPosition = pivot;

        spriteRenderer.transform.localScale = new Vector3(gridObjectData.size.x, gridObjectData.size.y, 1);
    }

    public void LateUpdate()
    {
        if (followCursor)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);

            Vector3 pivotPoint = mousePos;
            Vector2Int gridPoint = GridManager.Instance.grid.GetGridPosition(pivotPoint) - _gridObjectData.size / 2;
            Vector3 gridWorldPos = GridManager.Instance.grid.GetWorldPosition(gridPoint + _gridObjectData.size / 2);

            transform.position = gridWorldPos;

            bool canPlace = GridManager.Instance.grid.CanPlaceOnGrid(gridPoint, _gridObjectData.size);

            if (canPlace)
            {
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }


            if (Input.GetMouseButtonDown(0) && !Extension.IsPointerOverUIElement())
            {
                if (canPlace)
                {
                    _placementPivot = gridPoint;
                    followCursor = false;

                    switch (_gridObjectData.objectType)
                    {
                        case ObjectType.ActiveBuilding:
                            ActiveBuilding activeBuilding = new ActiveBuilding();
                            activeBuilding.productionData = _gridObjectData.productionData.productionData;
                            activeBuilding.view = gameObject;
                            activeBuilding.gridObjectData = _gridObjectData;
                            activeBuilding.gridPivot = _placementPivot;
                            activeBuilding.health = _gridObjectData.stats.health;
                            GridManager.Instance.grid.PlaceOnGrid(gridPoint, _gridObjectData.size, activeBuilding);

                            break;
                        case ObjectType.PassiveBuilding:
                            PassiveBuilding passiveBuilding = new PassiveBuilding();
                            passiveBuilding.view = gameObject;
                            passiveBuilding.gridObjectData = _gridObjectData;
                            passiveBuilding.gridPivot = _placementPivot;
                            passiveBuilding.health = _gridObjectData.stats.health;
                            GridManager.Instance.grid.PlaceOnGrid(gridPoint, _gridObjectData.size, passiveBuilding);
                            break;
                        case ObjectType.Unit:
                            Unit unit = new Unit();
                            unit.view = gameObject;
                            unit.gridObjectData = _gridObjectData;
                            unit.gridPivot = _placementPivot;
                            unit.health = _gridObjectData.stats.health;
                            unit.attackDamage = _gridObjectData.stats.damage;
                            GridManager.Instance.grid.PlaceOnGrid(gridPoint, _gridObjectData.size, unit);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}