using System;
using UnityEngine;

namespace Lionsfall
{

    public abstract class GridCell : MonoBehaviour
    {
        public Vector2Int coordinates;
        public Transform elementSpawnPoint;

        internal GridElement gridElement;

        public abstract void Initialize(CellData cellData);
    }
}