using UnityEngine;

namespace Lionsfall
{
    public class Player : Creature
    {
        public override bool IsPlayer { get; set; } = true;
        public override int TurnSpeed { get; set; } = 1;

        public override Vector2Int GetTargetCoordinates()
        {
        }
        public override void OnStartOfTurn()
        {

        }
    }
}