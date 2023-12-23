using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
        
        UpdateVisual();
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= OnSelectedUnitChanged;
    }

    private void OnSelectedUnitChanged(object sender, EventArgs _)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        meshRenderer.enabled = UnitActionSystem.Instance.SelectedUnit.Equals(unit);
    }
}
