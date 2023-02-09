using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    
    public event EventHandler OnSelectedUnitChanged;
    
    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private Unit selectedUnit;

    private bool isBusy;

    public Unit SelectedUnit => selectedUnit;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There are more than one UnitActionSystem!! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Update()
    {
        if (isBusy) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            /*if (SelectedUnit == null) return;*/

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.MoveAction.IsValidGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedUnit.MoveAction.Move(mouseGridPosition, ClearBusy);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.SpinAction.Spin(ClearBusy);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayer))
        {
            if (raycastHit.transform.TryGetComponent(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
}
