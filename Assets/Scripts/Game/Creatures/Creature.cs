using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Lionsfall
{
    public abstract class Creature : GridElement
    {
        public int initialHealth = 1;

        public abstract bool IsPlayer { get; set; }
        public abstract int TurnSpeed { get; set; }

        internal int health;
        private void Start()
        {
            health = initialHealth;
        }
        private void OnEnable()
        {
            EventManager.StartListening(Const.GameEvents.TURN_PASSED, OnTurnPassed);
        }
        private void OnDisable()
        {
            EventManager.StopListening(Const.GameEvents.TURN_PASSED, OnTurnPassed);
        }
        public void OnTurnPassed(EventParam e)
        {
            MoveTowardsTargetedCell();
            OnStartOfTurn();
        }

        public void MoveTowardsTargetedCell()
        {
            Vector2Int targetCoord = GetTargetCoordinates();

            List<Vector2Int> path = GridSystem.Instance.FindPath(parentCell.coordinates, targetCoord);
            // Make the creature jump to the path[0] cell's position.
            if (path.Count > 0)
            {
                Vector3 targetPosition = GridSystem.Instance.grid[path[0].x, path[0].y].transform.position;

                // Before the jump, set the parent cell to the new cell.
                if (parentCell != null)
                {
                    parentCell.gridElement = null;
                }
                parentCell = GridSystem.Instance.grid[path[0].x, path[0].y];
                parentCell.gridElement = this;

                transform.DOJump(targetPosition, 1f, 1, 0.5f);
            }
        }

        public abstract Vector2Int GetTargetCoordinates();
        public abstract void OnStartOfTurn();
    }

    public class GenericEnemy : Creature
    {

        public override bool IsPlayer { get; set; } = false;
        public override int TurnSpeed { get; set; } = 1;

        public override Vector2Int GetTargetCoordinates()
        {
            throw new System.NotImplementedException();
        }
        public override void OnStartOfTurn()
        {

        }
    }
}