using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Lionsfall
{
    public class RotateClockwiseAction : ActionBarItem
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
            Player.Instance.transform.Rotate(Vector3.forward, -90);
        }
    }
}