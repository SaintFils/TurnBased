using System;
using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        [SerializeField] private GameObject selectedOutline;
        [SerializeField] private GameObject notAvailableOutline;

        private BaseAction baseAction;
        private Color initialTextColor;

        private void Start()
        {
            initialTextColor = textMeshPro.color;

            TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        }

        private void OnDisable()
        {
            TurnSystem.Instance.OnTurnChanged -= OnTurnChanged;
        }

        public void SetBaseAction(BaseAction baseAction)
        {
            this.baseAction = baseAction;
            
            textMeshPro.text = baseAction.ActionName;
            
            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }

        public void UpdateSelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.SelectedAction;
            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;

            if(selectedUnit.CanSpendActionPointsToTakeActions(selectedBaseAction))
            {
                selectedOutline.SetActive(selectedBaseAction == baseAction);
                notAvailableOutline.SetActive(false);
            }
            else
            {
                selectedOutline.SetActive(false);
                notAvailableOutline.SetActive(selectedBaseAction == baseAction);
            }
            
        }

        public void SetDisabled(bool isDisabled)
        {
            if (isDisabled)
            {
                button.interactable = false;

                var textColor = textMeshPro.color;
                textColor.a = 0.25f;
                textMeshPro.color = textColor;
                
                selectedOutline.SetActive(false);
                notAvailableOutline.SetActive(false);
            }
            else
            {
                button.interactable = true;
                textMeshPro.color = initialTextColor;
            }
        }

        private void OnTurnChanged(object sender, EventArgs e) => UpdateSelectedVisual();
    }
}
