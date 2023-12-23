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

        private BaseAction baseAction;
        private Color initialTextColor;


        private void Start()
        {
            initialTextColor = textMeshPro.color;
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
            selectedOutline.SetActive(selectedBaseAction == baseAction);
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
            }
            else
            {
                button.interactable = true;
                textMeshPro.color = initialTextColor;
            }
        }
    }
}
