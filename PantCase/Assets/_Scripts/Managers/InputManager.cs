using System;
using System.Linq;
using _Scripts.Managers;
using Giroo.Utility;
using UnityEngine;


public class InputManager : Singleton<MonoBehaviour>
{
    private bool _isSelected;
    private GridObject _selectedGridObject;
    private ObjectType _selectedObjectType;


    public void Start()
    {
        EventManager.Instance.OnSelectedSpawn += SpawnSelected;
    }

    private void SpawnSelected(GridObjectData obj)
    {
        IProducer producer = (IProducer)_selectedGridObject;
        producer.Spawn(obj);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Extension.IsPointerOverUIElement())
        {
            #region Selection

            if (!_isSelected)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);

                if (GridManager.Instance.grid.IsInGrid(mousePos))
                {
                    Vector2Int gridPoint = GridManager.Instance.grid.GetGridPosition(mousePos);

                    if (GridManager.Instance.grid.grid[gridPoint.x, gridPoint.y].CellState != CellState.Empty)
                    {
                        Debug.Log("Clicked on cell with state: " + GridManager.Instance.grid.grid[gridPoint.x, gridPoint.y].CellObject.gridObjectData.name);
                        GridObject gridObject = GridManager.Instance.grid.grid[gridPoint.x, gridPoint.y].CellObject;

                        switch (gridObject.gridObjectData.objectType)
                        {
                            case ObjectType.ActiveBuilding:
                                ActiveBuilding activeBuilding = (ActiveBuilding)gridObject;
                                Debug.Log("Active building with production: " + activeBuilding.productionData.ToList());
                                SetSelected(activeBuilding, ObjectType.ActiveBuilding);
                                break;
                            case ObjectType.PassiveBuilding:
                                PassiveBuilding passiveBuilding = (PassiveBuilding)gridObject;
                                Debug.Log("Passive building with production: ");
                                SetSelected(passiveBuilding, ObjectType.PassiveBuilding);
                                break;
                            case ObjectType.Unit:
                                Unit unit = (Unit)gridObject;
                                Debug.Log("Unit ");
                                SetSelected(unit, ObjectType.Unit);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        Debug.Log("Clicked on empty cell");
                    }
                }
            }

            #endregion

            else
            {
                UnSelected();
            }
        }

        if (Input.GetMouseButtonDown(1) && _isSelected && !Extension.IsPointerOverUIElement())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = Extension.RayToPointAtZ(ray, 0);


            switch (_selectedObjectType)
            {
                case ObjectType.ActiveBuilding:
                case ObjectType.PassiveBuilding:


                    if (GridManager.Instance.grid.IsInGrid(mousePos))
                    {
                        Vector2Int gridPos = GridManager.Instance.grid.GetGridPosition(mousePos);

                        if (!GridManager.Instance.grid.grid[gridPos.x, gridPos.y].CanMove())
                        {
                            UnSelected();
                        }
                    }

                    break;
                case ObjectType.Unit:


                    Unit unit = (Unit)_selectedGridObject;

                    if (GridManager.Instance.grid.IsInGrid(mousePos))
                    {
                        Vector2Int gridPos = GridManager.Instance.grid.GetGridPosition(mousePos);
                        var canPlace = GridManager.Instance.grid.CanPlaceOnGrid(gridPos, unit.gridObjectData.size);

                        if (canPlace)
                        {
                            MovementManager.Instance.AddMoveable(unit, gridPos);
                            UnSelected();
                        }
                        else
                        {
                            //There is something  check can attack 

                            var gridObject = GridManager.Instance.grid.grid[gridPos.x, gridPos.y].CellObject as IDamagable;

                            if (gridObject != null)
                            {
                                var attacker = _selectedGridObject as IAttacker;
                                gridObject.TakeDamage(unit.GetAttackPower());
                                Debug.Log("Attacked " + gridObject + " with " + attacker + " for " + attacker.GetAttackPower() + " damage");
                            }

                            UnSelected();
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void SetSelected(GridObject gridObject, ObjectType objectType)
    {
        _selectedGridObject = gridObject;
        _selectedObjectType = objectType;
        _isSelected = true;
        _selectedGridObject.view.spriteRenderer.color = Color.green;

        if (_selectedObjectType == ObjectType.ActiveBuilding)
        {
            EventManager.Instance.ProducerSelected(gridObject as IProducer, gridObject.gridObjectData);
        }
    }

    public void UnSelected()
    {
        _selectedGridObject.view.spriteRenderer.color = Color.white;
        _isSelected = false;
        EventManager.Instance.UnSelected();
    }
}