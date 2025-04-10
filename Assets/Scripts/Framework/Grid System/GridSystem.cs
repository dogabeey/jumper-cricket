using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GridCell gridCellPrefab;
    public Transform gridParent;
    public int gridWidth;
    public int gridHeight;
    public Vector2 gridDistance = new Vector2(1, 1);


    internal GridCell[,] gridCells;

    public void GenerateGrid(CellData[,] cellData)
    {
        gridCells = new GridCell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * gridDistance.x, 0, y * gridDistance.y);
                GridCell cell = Instantiate(gridCellPrefab, position, Quaternion.identity, gridParent);
                cell.coordinates = new Vector2Int(x, y);
                gridCells[x, y] = cell;
                cell.Initialize(cellData[x, y]); // Initialize the cell with the data from the array
            }
        }
    }

    public void CentralizeGrid()
    {
        Vector3 center = new Vector3((gridWidth - 1) * gridDistance.x / 2, 0, (gridHeight - 1) * gridDistance.y / 2);
        gridParent.position = center;
    }
}
