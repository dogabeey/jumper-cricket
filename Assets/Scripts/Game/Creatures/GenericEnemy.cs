using UnityEngine;

namespace Lionsfall
{
    public class GenericEnemy : Creature
    {

        public override bool IsPlayer { get; set; } = false;
        public override int TurnSpeed { get; set; } = 1;

        public override Vector2Int GetTargetCoordinates()
        {
            Player player = LevelScene.Instance.player;
            return player.parentCell.coordinates;
        }
        public override void OnStartOfTurn()
        {

        }
    }
}