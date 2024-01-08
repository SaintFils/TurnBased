using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    
    private int turnNumber = 1;

    public int TurnNumber => turnNumber;

    public event EventHandler OnTurnChanged;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There are more than one TurnSystem!! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    

    public void NextTurn()
    {
        turnNumber++;
        
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
}
