using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using DG.Tweening;

namespace Lionsfall
{
    public class RotateClockwiseAction : ActionBarItem
    {
        public override float CostMultiplier => 0.0f; // No cost for this action
        public bool isRotating = false;

        public override bool IsClickable()
        {
            return !isRotating;
        }

        public override bool IsVisible()
        {
            return true;
        }

        public override void OnClick()
        {
            isRotating = true;
            Player.Instance.transform.DORotate(new Vector3(0, 90, 0), 0.5f).SetRelative();
            Player.Instance.transform.DOJump(Player.Instance.transform.position, 0.5f, 1, 0.5f).OnComplete(() => isRotating = false);
        }
    }
}