using UnityEngine;

namespace Lionsfall
{
    public abstract class GridElement : MonoBehaviour
    {
        public string elementName;
        public GridCell parentCell;
        public Texture2D editorIcon;
    }
}