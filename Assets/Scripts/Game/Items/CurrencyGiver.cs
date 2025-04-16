using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Lionsfall
{
    public class CurrencyGiver : Item
    {
        [ValueDropdown(nameof(GetCurrencyNames))]
        public string currencyName;
        public override void OnPickup()
        {
            CurrencyManager.Instance.AddCurrency("Key", 1, gameObject);
        }

        private List<string> GetCurrencyNames()
        {
            return CurrencyManager.Instance.currencyInfos
                .Select(x => x.currencyModel.currencyID)
                .ToList().Append("Coin").ToList();
        }
    }
}