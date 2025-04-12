using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public abstract void OnStartOfTurn();
    }

    public class GenericEnemy : Creature
    {
        public override Texture2D editorIcon => Resources.Load<Texture2D>("editor_icons/creatures/enemy");

        public override bool IsPlayer { get; set; } = false;
        public override int TurnSpeed { get; set; } = 1;

        public override void OnStartOfTurn()
        {

        }
    }
}