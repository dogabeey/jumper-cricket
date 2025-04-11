using UnityEngine;

namespace Lionsfall
{
    public class JumperShooterCell : GridCell
    {
        public CellType cellType;
        public GameObject platformRenderer;
        public GameObject wallRenderer;
        public GameObject exitRenderer;

        public override void Initialize(CellData cellData)
        {
            if(cellData is JumperShooterCellData jumperShooterCellData)
            {
                switch (jumperShooterCellData.cellType)
                {
                    case CellType.Empty:
                        cellType = CellType.Empty;
                        platformRenderer.SetActive(true);
                        wallRenderer.SetActive(false);
                        exitRenderer.SetActive(false);
                        break;
                    case CellType.Wall:
                        cellType = CellType.Wall;
                        platformRenderer.SetActive(true);
                        wallRenderer.SetActive(true);
                        exitRenderer.SetActive(false);
                        break;
                    case CellType.Exit:
                        cellType = CellType.Exit;
                        platformRenderer.SetActive(false);
                        wallRenderer.SetActive(false);
                        exitRenderer.SetActive(true);
                        break;
                    case CellType.Hole:
                        cellType = CellType.Hole;
                        platformRenderer.SetActive(false);
                        wallRenderer.SetActive(false);
                        exitRenderer.SetActive(false);
                        break;
                    default:
                        cellType = CellType.Hole;
                        platformRenderer.SetActive(false);
                        wallRenderer.SetActive(false);
                        exitRenderer.SetActive(false);
                        break;
                }
            }
        }
    }
}