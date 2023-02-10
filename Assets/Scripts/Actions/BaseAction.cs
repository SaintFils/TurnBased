using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        [SerializeField] protected string actionName;
        
        protected bool isActive;
        protected Unit unit;
        protected Action onActionComplete;

        public string ActionName => actionName;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        protected void ActionNameValidation(string nameText)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                actionName = nameText;
            }
        }
        
        public virtual bool IsValidGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validPositions = GetValidActionGridPositionList();
            return validPositions.Contains(gridPosition);
        }

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
        public abstract List<GridPosition> GetValidActionGridPositionList();
        
    }
}
