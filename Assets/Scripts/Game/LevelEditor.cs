using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;
using Sirenix.Utilities;

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

#if UNITY_EDITOR

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
            Rect iconRect = new Rect(rect.x + rect.width * 0.1f, rect.y + rect.height * 0.1f, rect.width * 0.8f, rect.height * 0.8f);

            // DRAWING
            Color color = Color.white;
            switch (value.cellType)
            {
                case CellType.Hole:
                    color = new Color(0.7f, 0.7f, 0.7f);
                    break;
                case CellType.Empty:
                    color = new Color(0.9f, 1f, 0.8f);
                    break;
                case CellType.Wall:
                    color = new Color(0.545f, 0.271f, 0.075f);
                    break;
                case CellType.Exit:
                    color = Color.green;
                    break;
            }
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, color, 0, 0);

            if (value.initialElement != null && value.initialElement.editorIcon != null)
            {
                GUI.DrawTexture(iconRect, value.initialElement.editorIcon, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
            }

            // Draw the direction arrow
            if (value.initialElement)
            {

                if (value.direction == Direction.Top)
                {
                    GUI.DrawTexture(iconRect, WorldManager.Instance.arrowUp, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
                }
                else if (value.direction == Direction.Right)
                {
                    GUI.DrawTexture(iconRect, WorldManager.Instance.arrowRight, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
                }
                else if (value.direction == Direction.Bottom)
                {
                    GUI.DrawTexture(iconRect, WorldManager.Instance.arrowDown, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
                }
                else if (value.direction == Direction.Left)
                {
                    GUI.DrawTexture(iconRect, WorldManager.Instance.arrowLeft, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
                }
            }

            // EVENTS
            Event evt = Event.current;

            if (rect.Contains(evt.mousePosition))
            {
                // Key press for changing cell type
                if (evt.type == EventType.KeyDown)
                {
                    if (evt.keyCode == HOLE_KEY)
                    {
                        value.cellType = CellType.Hole;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == EMPTY_KEY)
                    {
                        value.cellType = CellType.Empty;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == WALL_KEY)
                    {
                        value.cellType = CellType.Wall;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == EXIT_KEY)
                    {
                        value.cellType = CellType.Exit;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if(evt.keyCode == KeyCode.Delete || evt.keyCode == KeyCode.Backspace)
                    {
                        value.initialElement = null;
                        GUI.changed = true;
                        evt.Use();
                    }

                    else if(evt.keyCode == KeyCode.UpArrow)
                    {
                        value.direction = Direction.Top;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == KeyCode.RightArrow)
                    {
                        value.direction = Direction.Right;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == KeyCode.DownArrow)
                    {
                        value.direction = Direction.Bottom;
                        GUI.changed = true;
                        evt.Use();
                    }
                    else if (evt.keyCode == KeyCode.LeftArrow)
                    {
                        value.direction = Direction.Left;
                        GUI.changed = true;
                        evt.Use();
                    }
                }

                // Proper Drag & Drop support
                if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
                {
                    if (DragAndDrop.objectReferences.Length > 0)
                    {
                        // Check if the dragged object has any components that is derived from GridElement
                        bool isValidDrag = DragAndDrop.objectReferences[0].GetComponent<GridElement>() != null;
                        if (isValidDrag)
                            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                        else
                            DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;

                        if (evt.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();
                            GridElement element;
                            foreach (var obj in DragAndDrop.objectReferences)
                            {
                                if ( element = obj.GetComponent<GridElement>())
                                {
                                    value.initialElement = element;
                                    GUI.changed = true;
                                    evt.Use();
                                    break;
                                }
                            }
                        }

                        evt.Use(); // Prevent further event propagation
                    }
                }
            }

            return value;
        }
#endif

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
        public Direction direction = Direction.Top;
    }

    public enum CellType
    {
        Hole,
        Empty,
        Wall,
        Exit
    }
}
