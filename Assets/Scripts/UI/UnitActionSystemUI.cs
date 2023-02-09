using System;
using Actions;
using UnityEngine;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;
    
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
            CreateUnitActionButtons();
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform actionButton in actionButtonContainer)
            {
                Destroy(actionButton.gameObject);
            }
        
            Unit unit = UnitActionSystem.Instance.SelectedUnit;

            foreach (BaseAction action in unit.BaseActions)
            {
                Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainer);
                ActionButtonUI actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(action);
            }
        }
    
    
        private void OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
        }
    }
}
