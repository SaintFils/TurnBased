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

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.ActionName;
            
            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }
    }
}
