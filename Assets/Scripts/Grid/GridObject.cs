using System.Collections.Generic;

namespace Grid
{
    public class GridObject
    {
        private GridPosition gridPosition;
        private GridSystem gridSystem;
        private List<Unit> unitList;

        public List<Unit> UnitList => unitList;

        public GridObject(GridSystem system, GridPosition position)
        {
            this.gridSystem = system;
            this.gridPosition = position;

            unitList = new List<Unit>();
        }

        public override string ToString()
        {
            string unitString = "";
            
            foreach (Unit unit in unitList)
            {
                unitString += unit + "\n";
            }
            
            return gridPosition.ToString() + "\n" + unitString;
        }

        public void AddUnit(Unit unit)
        {
            unitList.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            unitList.Remove(unit);
        }
    }
}
