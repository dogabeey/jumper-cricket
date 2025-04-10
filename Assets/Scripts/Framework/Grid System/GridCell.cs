using System;
using UnityEngine;

public abstract class GridCell : MonoBehaviour
{
    public Vector2Int coordinates;
    public IGridElement gridElement;

    public abstract void Initialize(CellData cellData);
}
