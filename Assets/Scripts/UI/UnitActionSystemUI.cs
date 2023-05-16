using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;

        private List<ActionButtonUI> actionButtonUIList;

        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
            
            CreateUnitActionButtons();
            UpdateSelectedVisual();
        }

        private void OnDisable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged -= OnSelectedActionChanged;
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform actionButton in actionButtonContainer)
            {
                Destroy(actionButton.gameObject);
            }
            
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
        }

        private void OnSelectedActionChanged(object sender, EventArgs e)
        {
            UpdateSelectedVisual();
        }

        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                actionButtonUI.UpdateSelectedVisual();
            }
        }
    }
}
