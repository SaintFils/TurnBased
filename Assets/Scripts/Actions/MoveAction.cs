using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private float rotationSpeed = 10.0f;
        [SerializeField] private Animator unitAnimator;

        private Vector3 targetPosition;
        private Unit unit;

        private void Awake()
        {
            unit = GetComponent<Unit>();
            targetPosition = transform.position;
        }

        private void Update()
        {
            float stoppingDistance = .1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }
        }

        public void Move(GridPosition gridPosition)
        {
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
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