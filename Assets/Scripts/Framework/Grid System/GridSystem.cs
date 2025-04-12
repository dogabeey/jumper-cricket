using UnityEngine;

namespace Lionsfall
{
    public class GridSystem : MonoBehaviour
    {
        public GridCell gridCellPrefab;
        public Transform gridParent;
        public Vector2 gridDistance = new Vector2(1, 1);


        internal GridCell[,] gridCells;

        private void Start()
        {
            GenerateGrid(LevelScene.Instance.levelEditor.cellData);
            CenterGrid();
            LevelScene.Instance.levelEditor.SetCamera();
        }

        public void GenerateGrid(CellData[,] cellData)
        {
            gridCells = new GridCell[cellData.GetLength(0), cellData.GetLength(1)];

            for (int x = 0; x < cellData.GetLength(0); x++)
            {
                for (int y = 0; y < cellData.GetLength(1); y++)
                {
                    Vector3 position = new Vector3(x * gridDistance.x, 0, y * gridDistance.y);
                    GridCell cell = Instantiate(gridCellPrefab, position, Quaternion.identity, gridParent);
                    cell.coordinates = new Vector2Int(x, y);
                    gridCells[x, y] = cell;
                    cell.Initialize(cellData[x, y]); // Initialize the cell with the data from the array
                }
            }
        }

        // Move the center of the grid to (0,0,0) world position
        public void CenterGrid()
        {
            Vector3 center = new Vector3((gridCells.GetLength(0) - 1) * gridDistance.x / 2, 0, (gridCells.GetLength(1) - 1) * gridDistance.y / 2);
            for (int x = 0; x < gridCells.GetLength(0); x++)
            {
                for (int y = 0; y < gridCells.GetLength(1); y++)
                {
                    gridCells[x, y].transform.position -= center;
                }
            }
        }
    }
}