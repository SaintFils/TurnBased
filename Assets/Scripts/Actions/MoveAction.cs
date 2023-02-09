using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class MoveAction : BaseAction
    {
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private float rotationSpeed = 10.0f;
        [SerializeField] private Animator unitAnimator;

        private Vector3 targetPosition;
        
        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }

        private void Update()
        {
            if (!isActive) return;
            
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            
            float stoppingDistance = .1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);

                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
                isActive = false;
            }
            
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }

        public void Move(GridPosition gridPosition)
        {
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            isActive = true;
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPosition = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GridPosition;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (testGridPosition == unitGridPosition) continue;
                    if (LevelGrid.Instance.IsGridPositionOccupied(testGridPosition)) continue;
                    
                    validGridPosition.Add(testGridPosition);
                }
            }

            return validGridPosition;
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validPositions = GetValidActionGridPositionList();
            return validPositions.Contains(gridPosition);
        }
    }
}