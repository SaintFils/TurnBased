using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;

    public GridObject(GridSystem system, GridPosition position)
    {
        this.gridSystem = system;
        this.gridPosition = position;
    }

}
