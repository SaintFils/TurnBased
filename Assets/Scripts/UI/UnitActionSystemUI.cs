using System;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;
        [SerializeField] private TextMeshProUGUI actionPointsText;

        private List<ActionButtonUI> actionButtonUIList;

        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
            UnitActionSystem.Instance.OnBusyChanged += OnBusyChanged;
            UnitActionSystem.Instance.OnActionStarted += OnActionStarted;

            UpdateActionPoints();
            CreateUnitActionButtons();
            UpdateSelectedVisual();
        }

        private void OnDisable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged -= OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStarted -= OnActionStarted;
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform actionButton in actionButtonContainer)
                Destroy(actionButton.gameObject);
            
            actionButtonUIList.Clear();
        
            Unit unit = UnitActionSystem.Instance.SelectedUnit;

            foreach (BaseAction action in unit.BaseActions)
            {
                Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainer);
                ActionButtonUI actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(action);
                
                actionButtonUIList.Add(actionButtonUI);
            }
        }
    
        private void OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void OnSelectedActionChanged(object sender, EventArgs e) => UpdateSelectedVisual();

        private void OnActionStarted(object sender, EventArgs e) => UpdateActionPoints();

        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
                actionButtonUI.UpdateSelectedVisual();
        }
        
        private void OnBusyChanged(object sender, bool isBusy)
        {
            foreach (ActionButtonUI button in actionButtonUIList)
            {
                button.SetDisabled(isBusy);
                if(!isBusy)
                    button.UpdateSelectedVisual();
            }
        }

        private void UpdateActionPoints()
        {
            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            actionPointsText.text = $"Action points: {selectedUnit.ActionPoints}";
        }
    }
}
