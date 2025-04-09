using UnityEngine;
using TMPro;

namespace Lionsfall
{
    [CreateAssetMenu(fileName = "CurrencyModel", menuName = "Scriptable Objects/Currency Model")]
    public class CurrencyModel : ScriptableObject
    {
        public string currencyID;
        public float startingAmount;
        public SpriteRenderer currencySpritePrefab;
    }
}
