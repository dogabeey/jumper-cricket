using UnityEngine;

namespace Lionsfall
{
    public abstract class GridElement : MonoBehaviour
    {
        public string elementName;
        public GridCell parentCell;

        public abstract Texture2D editorIcon { get; }
    }
}