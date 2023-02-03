namespace Grid
{
    public class GridObject
    {
        private GridPosition gridPosition;
        private GridSystem gridSystem;

        public GridObject(GridSystem system, GridPosition position)
        {
            this.gridSystem = system;
            this.gridPosition = position;
        }

        public override string ToString()
        {
            return gridPosition.ToString();
        }
    }
}
