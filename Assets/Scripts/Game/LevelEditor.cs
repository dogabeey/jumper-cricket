using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lionsfall
{

    [ShowOdinSerializedPropertiesInInspector, InlineEditor, ListDrawerSettings(DraggableItems = true)]
    [CreateAssetMenu(fileName = "New Level", menuName = "Lionsfall/New Level...")]
    public class LevelEditor : SerializedScriptableObject
    {
        [TableMatrix(SquareCells = true, DrawElementMethod = nameof(DrawCells))]
        public JumperShooterCellData[,] cellData;
        [FoldoutGroup("Camera Settings")]
        public Vector3 cameraPositionOffset;
        [FoldoutGroup("Camera Settings")]
        public Vector3 cameraRotationOffset;
        [FoldoutGroup("Camera Settings")]
        public float cameraOrthoSize = 10f;

        private KeyCode HOLE_KEY = KeyCode.H;
        private KeyCode EMPTY_KEY = KeyCode.E;
        private KeyCode WALL_KEY = KeyCode.W;
        private KeyCode EXIT_KEY = KeyCode.X;

        [Button]
        public void InitializeArray(Vector2Int arraySizes)
        {
            cellData = new JumperShooterCellData[arraySizes.x, arraySizes.y];
            for (int i = 0; i < arraySizes.x; i++)
            {
                for (int j = 0; j < arraySizes.y; j++)
                {
                    cellData[i, j] = new JumperShooterCellData();
                    cellData[i, j].cellType = CellType.Empty; // Default cell type
                }
            }
        }
        private JumperShooterCellData DrawCells(Rect rect, JumperShooterCellData value)
        {
            if (value == null)
            {
                value = new JumperShooterCellData();
            }

            // INITIALIZATION

            // DRAWING
            // Recolor rect based on cell type
            Color color = Color.white;
            switch (value.cellType)
            {
                case CellType.Hole:
                    color = new Color(0.7f, 0.7f, 0.7f);
                    break;
                case CellType.Empty:
                    color = Color.white;
                    break;
                case CellType.Wall:
                    color = new Color(0.545f, 0.271f, 0.075f);
                    break;
                case CellType.Exit:
                    color = Color.green;
                    break;
            }
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, color, 0, 0);

            // EVENTS
            if (rect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.KeyDown)
                {
                    // Left click
                    if (Event.current.keyCode == HOLE_KEY)
                    {
                        value.cellType = CellType.Hole;
                        GUI.changed = true;
                        Event.current.Use();
                    }
                    else if (Event.current.keyCode == EMPTY_KEY)
                    {
                        value.cellType = CellType.Empty;
                        GUI.changed = true;
                        Event.current.Use();
                    }
                    else if (Event.current.keyCode == WALL_KEY)
                    {
                        value.cellType = CellType.Wall;
                        GUI.changed = true;
                        Event.current.Use();
                    }
                    else if (Event.current.keyCode == EXIT_KEY)
                    {
                        value.cellType = CellType.Exit;
                        GUI.changed = true;
                        Event.current.Use();
                    }
                }
            }
            
            return value;
        }

        public void SetCamera()
        {
            Camera.main.transform.position += cameraPositionOffset;
            Camera.main.transform.eulerAngles += cameraRotationOffset;
            Camera.main.orthographicSize = cameraOrthoSize;
        }
    }
    [System.Serializable]
    public class JumperShooterCellData : CellData
    {
        public CellType cellType;
        public GridElement initialElement;
    }

    public enum CellType
    {
        Hole,
        Empty,
        Wall,
        Exit
    }
}
