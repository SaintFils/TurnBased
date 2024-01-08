using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ActionPointsMax = 2;

    public static event EventHandler OnAnyActionPointChanged;
    
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActions;
    private int actionPoints = ActionPointsMax;
    
    public GridPosition GridPosition => gridPosition;

    public MoveAction MoveAction => moveAction;
    public SpinAction SpinAction => spinAction;
    public BaseAction[] BaseActions => baseActions;
    public int ActionPoints => actionPoints;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActions = GetComponents<BaseAction>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.OnTurnChanged -= OnTurnChanged;
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);

            gridPosition = newGridPosition;
        }
    }

    public bool CanSpendActionPointsToTakeActions(BaseAction action) => actionPoints >= action.GetActionCost();
    
    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        
        OnAnyActionPointChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendActionPointToTakeAction(BaseAction action)
    {
        if (CanSpendActionPointsToTakeActions(action))
        {
            SpendActionPoints(action.GetActionCost());
            return true;
        }
        else
            return false;
    }

    private void OnTurnChanged(object sender, EventArgs e)
    {
        actionPoints = ActionPointsMax;
        
        OnAnyActionPointChanged?.Invoke(this, EventArgs.Empty);
    }
}