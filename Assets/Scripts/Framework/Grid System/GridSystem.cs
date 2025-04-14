using System.Collections.Generic;
using UnityEngine;

namespace Lionsfall
{
    public class GridSystem : SingletonComponent<GridSystem>
    {
        public GridCell gridCellPrefab;
        public Transform gridParent;
        public Vector2 gridDistance = new Vector2(1, 1);


        internal GridCell[,] grid;

        private void Start()
        {
            GenerateGrid(LevelScene.Instance.levelEditor.cellData);
            CenterGrid();
            LevelScene.Instance.levelEditor.SetCamera();
        }

        public void GenerateGrid(CellData[,] cellData)
        {
            grid = new GridCell[cellData.GetLength(0), cellData.GetLength(1)];

            for (int x = 0; x < cellData.GetLength(0); x++)
            {
                for (int y = 0; y < cellData.GetLength(1); y++)
                {
                    Vector3 position = new Vector3(x * gridDistance.x, 0, y * gridDistance.y);
                    GridCell cell = Instantiate(gridCellPrefab, position, Quaternion.identity, gridParent);
                    cell.coordinates = new Vector2Int(x, y);
                    grid[x, y] = cell;
                    cell.Initialize(cellData[x, y]); // Initialize the cell with the data from the array
                }
            }
        }

        // Move the center of the grid to (0,0,0) world position
        public void CenterGrid()
        {
            Vector3 center = new Vector3((grid.GetLength(0) - 1) * gridDistance.x / 2, 0, (grid.GetLength(1) - 1) * gridDistance.y / 2);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].transform.position -= center;
                }
            }
        }
        public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            var openSet = new PriorityQueue<Vector2Int>();
            var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            var gScore = new Dictionary<Vector2Int, int>();
            var fScore = new Dictionary<Vector2Int, int>();

            openSet.Enqueue(start, 0);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            while (openSet.Count > 0)
            {
                Vector2Int current = openSet.Dequeue();

                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!IsWalkable(neighbor))
                        continue;

                    int tentativeGScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }

            return null; // No path found
        }

        private int Heuristic(Vector2Int a, Vector2Int b)
        {
            // Manhattan Distance
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        private List<Vector2Int> GetNeighbors(Vector2Int cell)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            Vector2Int[] directions = {
            new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(0, 1), new Vector2Int(0, -1)
        };

            foreach (var dir in directions)
            {
                Vector2Int neighbor = cell + dir;
                if (IsInBounds(neighbor))
                    neighbors.Add(neighbor);
            }

            return neighbors;
        }

        private bool IsWalkable(Vector2Int coord)
        {
            return IsInBounds(coord) && grid[coord.x, coord.y].IsWalkable;
        }

        private bool IsInBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.y >= 0 &&
                   coord.x < grid.GetLength(0) && coord.y < grid.GetLength(1);
        }

        private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            while (cameFrom.ContainsKey(current))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Add(current); // Add the start position
            path.Reverse();
            return path;
        }

    }

}