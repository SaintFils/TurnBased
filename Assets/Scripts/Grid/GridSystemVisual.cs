using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }
        
        [SerializeField] private Transform gridVisualSinglePrefab;

        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"There are more than one GridSystemVisual!! {transform} - {Instance}");
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
        }

        private void Start()
        {
            gridSystemVisualSingleArray = new GridSystemVisualSingle[
                LevelGrid.Instance.GetWidth(),
                LevelGrid.Instance.GetHeight()
            ];
            
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridSystemVisualSingleTransform = Instantiate(gridVisualSinglePrefab, 
                        LevelGrid.Instance.GetWorldPosition(gridPosition),
                        Quaternion.identity);

                    gridSystemVisualSingleArray[x, z] =
                        gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPosition()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    gridSystemVisualSingleArray[x, z].Hide();
                }
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositions)
        {
            foreach (GridPosition position in gridPositions)
            {
                gridSystemVisualSingleArray[position.x, position.z].Show();
            }
        }

        private void UpdateGridVisual()
        {
            HideAllGridPosition();

            BaseAction selectedAction = UnitActionSystem.Instance.SelectedAction;
            ShowGridPositionList(selectedAction.GetValidActionGridPositionList());;
        }
    }
}
