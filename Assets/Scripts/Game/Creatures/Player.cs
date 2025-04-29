using UnityEngine;

namespace Lionsfall
{
    public class Player : Creature
    {
        public static Player Instance;

        public override bool IsPlayer { get; set; } = true;
        public override int TurnSpeed { get; set; } = 2;

        private void Awake()
        {
            Instance = this;
        }

        public override Vector2Int GetTargetCoordinates()
        {
            return parentCell.coordinates;
        }
        public override void OnStartOfTurn()
        {

        }
    }
}