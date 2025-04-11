using UnityEngine;

namespace Lionsfall
{
    public abstract class GridElement : MonoBehaviour
    {
        public GridCell parentCell;

        public abstract string ElementName { get; }
        public abstract Texture2D editorIcon { get; }
    }
}