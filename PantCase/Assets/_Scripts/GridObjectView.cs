using System;
using System.Text;
using Giroo.Utility;
using UnityEngine;

public class GridObjectView : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private GridObjectData _gridObjectData;

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
                    followCursor = false;
                    GridObject gridObject = new GridObject
                    {
                        view = gameObject,
                        gridObjectData = _gridObjectData
                    };
                    GridManager.Instance.grid.PlaceOnGrid(gridPoint, _gridObjectData.size, gridObject);
                }

                else
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}