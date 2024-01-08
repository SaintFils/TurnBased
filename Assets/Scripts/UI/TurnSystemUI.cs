using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;

    private void Start()
    {
        endTurnButton.onClick.AddListener((() => TurnSystem.Instance.NextTurn()));

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        
        UpdateTurnText();
    }

    private void OnDisable()
    {
        TurnSystem.Instance.OnTurnChanged -= OnTurnChanged;
    }

    private void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = $"TURN {TurnSystem.Instance.TurnNumber}";
    }
}
