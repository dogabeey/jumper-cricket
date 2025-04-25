using UnityEngine;

namespace Lionsfall
{
    public class RotateCounterClockwiseAction : ActionBarItem
    {
        public override float CostMultiplier => 0.0f; // No cost for this action

        public override bool IsClickable()
        {
            return true;
        }

        public override bool IsVisible()
        {
            return true;
        }

        public override void OnClick()
        {
            Player.Instance.transform.Rotate(Vector3.forward, 90);
        }
    }
}