using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += OnBusyChanged;
        
        Hide();
    }

    //private void OnDisable() => UnitActionSystem.Instance.OnBusyChanged -= OnBusyChanged;

    private void Show() => gameObject.SetActive(true);

    private void Hide() => gameObject.SetActive(false);
    

    private void OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
            Show();
        else
            Hide();
    }
}
