using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActions;
    private int actionPoints = 2;
    
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

    private bool CanSpendActionPointsToTakeActions(BaseAction action) => actionPoints >= action.GetActionCost();
    
    private void SpendActionPoints(int amount) => actionPoints -= amount;

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
}