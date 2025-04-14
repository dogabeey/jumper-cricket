using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lionsfall
{
    public abstract class Item : GridElement
    {
        public MeshRenderer itemRenderer;

        public abstract void OnPickup();

        private void Start()
        {
            itemRenderer.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        public void Pickup()
        {
            OnPickup();
            parentCell.gridElement = null;
            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            Pickup();
        }
    }
}